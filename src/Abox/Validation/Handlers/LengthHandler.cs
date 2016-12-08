using System;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Validation.Attributes;
using Abox.Validation.Messages;

namespace Abox.Validation.Handlers
{
    public class LengthHandler : Handler<ValidationMessage<Length>>
    {
        public override async Task Run(ValidationMessage<Length> message, IContext context)
        {
            var value = Convert.ToString(message.Value);

            if (message.Attribute.Min.HasValue &&
                value.Length < message.Attribute.Min.Value)
            {
                await context.Emit(new ValidationErrorMessage
                {
                    Property = message.Property,
                    Message = "Minimum length"
                });
            }

            if (message.Attribute.Max.HasValue &&
                value.Length > message.Attribute.Max.Value)
            {
                await context.Emit(new ValidationErrorMessage
                {
                    Property = message.Property,
                    Message = "Maximum length"
                });
            }
        }
    }
}
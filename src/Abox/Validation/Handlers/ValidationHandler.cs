using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Validation.Messages;

namespace Abox.Validation.Handlers
{
    public class ValidationHandler : Handler<object>
    {
        public override async Task Run(object message, IContext context)
        {
            var errors = new List<ValidationErrorMessage>();

            var properties = message
                .GetType()
                .GetProperties()
                .Select(p => new {Info = p, Attributes = p.GetCustomAttributes()});

            foreach (var property in properties)
            {
                foreach (var attribute in property.Attributes)
                {
                    if (!(attribute is Attributes.Validation))
                        continue;

                    var validationMessage = new ValidationMessage<Attributes.Validation>
                    {
                        Attribute = attribute as Attributes.Validation,
                        Property = property.Info.Name,
                        Value = property.Info.GetValue(message)
                    };

                    errors.AddRange((await context.Emit(validationMessage)).OfType<ValidationErrorMessage>());
                }
            }

            if (errors.Count > 0)
            {
                await context.End(new ValidationResultMessage
                {
                    Errors = errors
                });
            }
        }
    }
}
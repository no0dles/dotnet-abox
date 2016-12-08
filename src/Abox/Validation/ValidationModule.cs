using Abox.Core;
using Abox.Validation.Attributes;
using Abox.Validation.Handlers;
using Abox.Validation.Messages;

namespace Abox.Validation
{
    public class ValidationModule : Module
    {
        public override void Configure(IModuleBuilder builder)
        {
            builder.AddHandler<ValidationHandler, object>("*");

            builder.AddHandler<LengthHandler, ValidationMessage<Length>>();
        }
    }
}
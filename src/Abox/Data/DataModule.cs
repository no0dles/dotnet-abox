using Abox.Core;
using Abox.Data.Handlers;
using Abox.Data.Services;

namespace Abox.Data
{
    public class DataModule : Module
    {
        public override void Configure(IModuleBuilder builder)
        {
            builder.Dependencies.AddSingleton<DataService>();

            builder.AddHandler<AuthorizationHandler, object>("*");
        }
    }
}
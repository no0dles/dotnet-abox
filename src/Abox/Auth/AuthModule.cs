using Abox.Core;
using Abox.Auth.Handlers;
using Abox.Auth.Messages;
using Abox.Auth.Models;
using Abox.Auth.Services;

namespace Abox.Auth
{
    public class AuthModule : Module
    {
        public override void Configure(IModuleBuilder moduleBuilder)
        {
            moduleBuilder.Dependencies.AddSingleton<PermissionService>();
            moduleBuilder.Dependencies.AddSingleton(() => new Authorization());

            moduleBuilder.AddHandler<TokenHandler, TokenMessage>();
            moduleBuilder.AddHandler<PermissionHandler, object>("*");
            moduleBuilder.AddHandler<LoginHandler, LoginMessage>();
        }
    }
}
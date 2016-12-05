using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Auth.Messages;

namespace Abox.Auth.Handlers
{
    public class TokenHandler : Handler<TokenMessage>
    {
        [Inject]
        public DependencyManager Services { get; set; }

        public override Task Run(TokenMessage message, IContext context)
        {
            //check signature

            Services.AddSingleton(() => message.Auth);

            return Task.CompletedTask;
        }
    }
}
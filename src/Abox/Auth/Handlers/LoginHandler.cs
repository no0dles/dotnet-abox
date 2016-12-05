using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Auth.Messages;
using Abox.Auth.Models;
using Abox.Data.Messages;
using Abox.Data.Services;

namespace Abox.Auth.Handlers
{
    public class LoginHandler : Handler<LoginMessage>
    {
        [Inject]
        public DataService Data { get; set; }

        public override async Task Run(LoginMessage message, IContext context)
        {
            var document = await context
                .EmitOne<ReadDocumentResult<User>>(
                    new ReadDocument<User> {Id = message.Username});

            await context.Emit(new TokenMessage {
                Auth = new Authorization {
                    Username = message.Username,
                    Roles = { "admin" },
                    Anonymous = false
                },
                Signature = "abc" //create signature
            });
        }
    }
}
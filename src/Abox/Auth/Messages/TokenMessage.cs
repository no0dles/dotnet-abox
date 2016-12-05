using Abox.Core.Attributes;
using Abox.Auth.Attributes;
using Abox.Auth.Models;

namespace Abox.Auth.Messages
{
    [Message("auth.token")]
    [AuthorizeAnonymous]
    public class TokenMessage
    {
        public Authorization Auth { get; set; }

        public string Signature { get; set; }

        public TokenMessage()
        {
            Auth = new Authorization();
        }
    }
}
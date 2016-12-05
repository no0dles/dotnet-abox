using Abox.Core.Attributes;
using Abox.Auth.Attributes;

namespace Abox.Auth.Messages
{
    [Message("auth.login")]
    [AuthorizeAnonymous]
    public class LoginMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
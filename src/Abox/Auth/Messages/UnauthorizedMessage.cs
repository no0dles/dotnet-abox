using Abox.Auth.Attributes;
using Abox.Core.Attributes;
using Abox.Security.Attributes;

namespace Abox.Auth.Messages
{
    [Message("auth.unauthorized")]
    [Internal]
    [AuthorizeAnonymous]
    public class UnauthorizedMessage
    {
        public string Key { get; set; }
    }
}
using Abox.Auth.Models;

namespace Abox.Data.Attributes
{
    public abstract class BaseAnonymous : AuthorizationAttribute
    {
        public override bool IsAuthorized(object obj, Authorization auth)
        {
            return auth.Anonymous;
        }
    }
}
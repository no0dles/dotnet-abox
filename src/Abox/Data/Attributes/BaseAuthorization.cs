using Abox.Auth.Models;

namespace Abox.Data.Attributes
{
    public abstract class AuthorizationAttribute : System.Attribute
    {
        public abstract bool IsAuthorized(object obj, Authorization auth);
    }
}
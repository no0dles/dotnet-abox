using System.Linq;
using Abox.Auth.Models;

namespace Abox.Data.Attributes
{
    public abstract class BaseRole : AuthorizationAttribute
    {
        public string[] Roles { get; set; }

        public BaseRole(string[] roles)
        {
            Roles = roles;
        }

        public override bool IsAuthorized(object obj, Authorization auth)
        {
            return Roles.Any(role => auth.Roles.Any(authorizedRole => authorizedRole == role));
        }
    }
}
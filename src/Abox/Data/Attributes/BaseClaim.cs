using System.Linq;
using Abox.Auth.Models;

namespace Abox.Data.Attributes
{
    public abstract class BaseClaim : AuthorizationAttribute
    {
        public string[] Claims { get; set; }

        public BaseClaim(string[] claims)
        {
            Claims = claims;
        }

        public override bool IsAuthorized(object obj, Authorization auth)
        {
            return Claims.Any(claim => auth.Claims.Any(authorizedClaim => authorizedClaim == claim));
        }
    }
}
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Auth.Messages;
using Abox.Auth.Models;
using Abox.Auth.Services;

namespace Abox.Auth.Handlers
{
    public class PermissionHandler : Handler<object>
    {
        [Inject]
        public Authorization Auth { get; set; }

        [Inject]
        public PermissionService Permission { get; set; }

        private bool IsPermitted(object action)
        {
            var permission = Permission.Resolve(action.GetType());

            if(permission.Anonymous)
                return true;

            if(permission.Claims.Any(claim => Auth.Claims.Contains(claim)))
                return true;

            if(permission.Roles.Any(role => Auth.Roles.Contains(role)))
                return true;

            return false;
        }

        public override async Task Run(object action, IContext context)
        {
            if (!IsPermitted(action))
            {
                await context.End(new UnauthorizedMessage
                {
                    Key = $"No permission to execute '{action}'"
                });
            }
        }
    }
}
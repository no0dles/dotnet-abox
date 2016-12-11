using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Auth.Messages;
using Abox.Auth.Models;
using Abox.Auth.Services;
using Abox.Security.Attributes;

namespace Abox.Auth.Handlers
{
    public class PermissionHandler : Handler<object>
    {
        [Inject]
        public Authorization Auth { get; set; }

        [Inject]
        public PermissionService Permission { get; set; }

        private bool IsPermitted(object message)
        {
            var permission = Permission.Resolve(message.GetType());

            if (permission.Anonymous)
                return true;

            if (permission.Claims.Any(claim => Auth.Claims.Contains(claim)))
                return true;

            if (permission.Roles.Any(role => Auth.Roles.Contains(role)))
                return true;

            return false;
        }

        private bool isInternal(object message)
        {
            var internalAttribute = message
                .GetType()
                .GetTypeInfo()
                .GetCustomAttribute<Internal>();

            return internalAttribute != null;
        }

        public override async Task Run(object message, IContext context)
        {
            if (isInternal(message))
                return;

            if (IsPermitted(message))
                return;

            await context.End(new UnauthorizedMessage
            {
                Key = $"No permission to execute '{message}'"
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abox.Auth.Models;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Data.Attributes;
using Abox.Data.Messages;

namespace Abox.Data.Handlers
{
    public class AuthorizationHandler : Handler<object>
    {
        [Inject]
        public Authorization Auth { get; set; }

        private bool IsAuthorized(object instance)
        {
            if (instance == null)
                return true;

            var type = instance.GetType();

            if (type.GetTypeInfo().IsValueType)
                return true;

            var properties = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            foreach (var property in properties)
            {
                var attributes = property.PropertyType
                    .GetTypeInfo()
                    .GetCustomAttributes();

                attributes.Concat(property.GetCustomAttributes());

                if (attributes
                    .OfType<AuthorizationAttribute>()
                    .Any(a => !a.IsAuthorized(instance, Auth)))
                    return false;

                if (property.PropertyType.GetTypeInfo().IsValueType)
                    continue;

                try
                {
                    if (!IsAuthorized(property.GetValue(instance)))
                        return false;
                }
                catch (Exception)
                {
                    //until GetIndexParameters() is implemented
                    continue;
                }
            }

            return true;
        }

        public override async Task Run(object message, IContext context)
        {
            if (!IsAuthorized(message))
                await context.End(new Unauthorized());
        }
    }
}
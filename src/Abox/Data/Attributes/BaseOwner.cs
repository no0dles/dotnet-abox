using System.Reflection;
using Abox.Auth.Models;

namespace Abox.Data.Attributes
{
    public abstract class BaseOwner : AuthorizationAttribute
    {
        public override bool IsAuthorized(object obj, Authorization auth)
        {
            var properties = obj
                .GetType()
                .GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<Owner>();
                if(attribute == null)
                    continue;

                var value = property.GetValue(obj);

                return auth.Username == value.ToString();
            }

            return false;
        }
    }
}
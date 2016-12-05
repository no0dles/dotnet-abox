using System;
using System.Collections.Generic;
using System.Reflection;
using Abox.Core.Attributes;

namespace Abox.Core
{
    public class DependencyManager
    {
        private readonly Dictionary<Type, Func<object>> dependencies = new Dictionary<Type, Func<object>>();

        public void AddSingleton<TInstance>()
                     where TInstance : new()
        {
            AddSingleton(() => new TInstance());
        }

        public void AddSingleton<TInstance>(Func<TInstance> factory)
        {
            var instance = factory();

            dependencies[typeof(TInstance)] = () => instance;
        }

        public void AddTransient<TInstance>()
            where TInstance : new()
        {
            dependencies[typeof(TInstance)] = () => new TInstance();
        }

        public TInstance Resolve<TInstance>(Type type)
            where TInstance : class, new()
        {

            if (!dependencies.ContainsKey(type))
                throw new Exception($"Could not resolve type '{type.FullName}'");

            var instance = dependencies[type]();

            foreach(var property in type.GetProperties())
            {
                var inject = property.GetCustomAttribute(typeof(Inject));

                if(inject == null)
                    continue;

                var injectInstance = Resolve<object>(property.PropertyType);

                property.SetValue(instance, injectInstance);
            }

            return instance as TInstance;
        }

        public TInstance Resolve<TInstance>()
            where TInstance : class, new()
        {
            return Resolve<TInstance>(typeof(TInstance));
        }
    }
}
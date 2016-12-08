using System;
using System.Reflection;
using Abox.Core.Attributes;

namespace Abox.Core
{
    public class ModuleBuilder : IModuleBuilder
    {
        private readonly Module module;

        public DependencyManager Dependencies => module.Dependencies;

        public ModuleBuilder(Module module)
        {
            this.module = module;
        }

        public virtual void AddHandler<THandler, TMessage>()
            where THandler : Handler<TMessage>, new()
            where TMessage : class, new()
        {
            var attributes = typeof(TMessage)
                .GetTypeInfo()
                .GetCustomAttributes<Message>();

            foreach(var attribute in attributes)
            {
                AddHandler<THandler, TMessage>(attribute.Name);
            }
        }

        public virtual void AddModule<TModule>()
            where TModule : Module, new()
        {
            var otherModule = new TModule();

            otherModule.Configure(this);
        }

        public virtual void AddHandler<THandler, TMessage>(string key)
            where THandler : Handler<TMessage>, new()
            where TMessage : class, new()
        {
            module.Dependencies.AddTransient<THandler>();

            module.Handles.Add(new Handle(key, () => Dependencies.Resolve<THandler>()));
        }
    }
}
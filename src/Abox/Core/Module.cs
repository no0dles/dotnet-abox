using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abox.Core.Messages;
using Abox.Security.Attributes;

namespace Abox.Core
{
    public abstract class Module
    {
        public Dictionary<string, Type> Messages { get; }
        public DependencyManager Dependencies { get; }
        public List<Handle> Handles { get; }

        protected Module()
        {
            Handles = new List<Handle>();
            Messages = new Dictionary<string, Type>();

            Dependencies = new DependencyManager();
            Dependencies.AddSingleton(() => Dependencies);
        }

        public abstract void Configure(IModuleBuilder builder);

        public void Configure()
        {
            Configure(new ModuleBuilder(this));
        }

        public async Task<IEnumerable<Message<TResponse>>> Run<TResponse>(Message<object> message)
            where TResponse : class
        {
            var context = new Context(this);

            foreach(var handle in Handles)
            {
                if(!handle.Matches(message.Key))
                    continue;

                await handle.Execute(message.Value, context);

                if(context.IsFinished)
                    break;
            }

            return context.Messages
                .Where(a => a.Value is TResponse)
                .Select(a => a as Message<TResponse>);
        }

        public async Task<IEnumerable<Message<object>>> Execute(IEnumerable<Message<object>> messages)
        {
            var result = new List<Message<object>>();

            foreach(var message in messages)
            {
                var attributes = message
                    .GetType()
                    .GetTypeInfo()
                    .GetCustomAttributes<Internal>();

                if (attributes.Any())
                {
                    result.Add(new Message<object> {
                        Key = "abox.unknown",
                        Value = new UnknownMessage { Key = message.Key }
                    });

                    continue;
                }

                var responses = await Run<object>(message);

                result.AddRange(responses);
            }

            return result;
        }
    }
}
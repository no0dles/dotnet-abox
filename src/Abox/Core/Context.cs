using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abox.Core.Attributes;

namespace Abox.Core
{
    public class Context : IContext
    {
        private readonly Module module;

        public List<Message<object>> Messages { get; }
        public bool IsFinished { get; private set; }

        public Context(Module module)
        {
            this.module = module;

            Messages = new List<Message<object>>();
            IsFinished = false;
        }

        public Task<IEnumerable<object>> Emit(string key, object data)
        {
            return Emit<object>(key, data);
        }

        public Task<IEnumerable<object>> Emit(object action)
        {
            return Emit<object>(action);
        }

        public async Task<IEnumerable<TResponse>> Emit<TResponse>(string key, object data)
            where TResponse : class
        {
            var action = new Message<object>
            {
                Key = key, 
                Value = data 
            };

            Messages.Add(action);

            return (await module.Run<TResponse>(action))
                .Select(a => a.Value);
        }

        public async Task<IEnumerable<TResponse>> Emit<TResponse>(object action)
            where TResponse : class
        {
            var responses = new List<TResponse>();

            var attributes = action
                .GetType()
                .GetTypeInfo()
                .GetCustomAttributes<Message>();

            foreach(var attribute in attributes)
                 responses.AddRange(await Emit<TResponse>(attribute.Name, action));

            return responses
                .Where(res => res is TResponse);
        }

        public Task End(string key, object data)
        {
            IsFinished = true;

            return Emit(key, data);
        }

        public Task End(object action)
        {
            IsFinished = true;

            return Emit(action);
        }

        public async Task<TResponse> EmitOne<TResponse>(string key, object data)
            where TResponse : class
        {
            return (await Emit<TResponse>(key, data))
                .FirstOrDefault();
        }

        public async Task<TResponse> EmitOne<TResponse>(object action)
            where TResponse : class
        {
            return (await Emit<TResponse>(action))
                .FirstOrDefault();
        }
    }
}
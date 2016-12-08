using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abox.Core.Attributes;

namespace Abox.Core
{
    public class Context : IContext
    {
        public Module Root { get; set; }
        public Context Parent { get; }

        public List<Message<object>> Messages { get; }

        public bool IsFinished { get; private set; }

        private int _level;

        public Context(Module root, Context parent = null, int level = 0)
        {
            Root = root;
            Parent = parent;

            _level = level;

            Messages = new List<Message<object>>();

            IsFinished = false;
        }

        public void AddMessage(Message<object> message)
        {
            Messages.Add(message);
            Parent?.AddMessage(message);
        }

        public async Task<IEnumerable<object>> Emit(string key, object data)
        {
            var message = new Message<object>
            {
                Key = key, 
                Value = data 
            };

            AddMessage(message);

            Console.WriteLine($"-{new string(' ', _level*3)} {key} - {message.Value.GetType().FullName}");

            foreach (var handle in Root.Resolve(key))
            {
                var context = new Context(Root, this, _level + 1);

                await handle.Execute(data, context);

                if(context.IsFinished)
                    break;

                await context.Emit(message);
            }

            return Messages.Select(m => m.Value);
        }

        public async Task<IEnumerable<object>> Emit(object action)
        {
            var responses = new List<object>();

            var attributes = action
                .GetType()
                .GetTypeInfo()
                .GetCustomAttributes<Message>();

            foreach(var attribute in attributes)
                 responses.AddRange(await Emit(attribute.Name, action));

            return responses;
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

        public async Task<object> EmitOne(string key, object data)
        {
            return (await Emit(key, data))
                .FirstOrDefault();
        }

        public async Task<object> EmitOne(object action)
        {
            return (await Emit(action))
                .FirstOrDefault();
        }
    }
}
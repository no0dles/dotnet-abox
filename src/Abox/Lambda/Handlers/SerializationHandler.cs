using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Lambda.Messages;
using Abox.Lambda.Services;

namespace Abox.Lambda.Handlers
{
    public class SerializationHandler : Handler<SerializationMessage>
    {
        [Inject]
        public SerializationService SerializationService { get; set; }

        public override async Task Run(SerializationMessage message, IContext context)
        {
            foreach (var unserializedMessage in message.Messages)
            {
                var type = SerializationService.Resolve(unserializedMessage.Key);
                var convertedMessage = new Message<object>
                {
                    Key = unserializedMessage.Key,
                    Value = unserializedMessage.Value.ToObject(type)
                };

                await context.Emit(convertedMessage.Key, convertedMessage.Value);
            }
        }
    }
}
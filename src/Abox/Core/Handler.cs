using System.Threading.Tasks;

namespace Abox.Core
{
    public abstract class Handler<TMessage> : IHandler
        where TMessage : class, new()
    {
        public abstract Task Run(TMessage message, IContext context);

        public Task Execute(object message, IContext context)
        {
            return Run(message as TMessage, context);
        }
    }
}
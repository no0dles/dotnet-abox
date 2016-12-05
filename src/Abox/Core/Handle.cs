using System;
using System.Threading.Tasks;

namespace Abox.Core
{
    public class Handle
    {
        private readonly string key;
        private readonly Func<IHandler> handler;

        public Handle(string key, Func<IHandler> handler)
        {
            this.key = key;
            this.handler = handler;
        }

        public bool Matches(string key)
        {
            if (this.key == "*")
                return true;

            return this.key == key;
        }

        public Task Execute(object data, Context context)
        {
            return handler().Execute(data, context);
        }
    }
}
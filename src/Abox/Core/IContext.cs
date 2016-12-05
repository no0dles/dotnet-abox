using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abox.Core
{
    public interface IContext 
    {
        Task<IEnumerable<object>> Emit(string key, object data);

        Task<IEnumerable<object>> Emit(object action);

        Task<IEnumerable<TResponse>> Emit<TResponse>(string key, object data)
            where TResponse : class;

        Task<IEnumerable<TResponse>> Emit<TResponse>(object action)
            where TResponse : class;

        Task<TResponse> EmitOne<TResponse>(string key, object data)
            where TResponse : class;

        Task<TResponse> EmitOne<TResponse>(object action)
            where TResponse : class;

        Task End(string key, object data);

        Task End(object action);
    }
}
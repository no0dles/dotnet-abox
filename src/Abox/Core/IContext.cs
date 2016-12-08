using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abox.Core
{
    public interface IContext 
    {
        Task<IEnumerable<object>> Emit(string key, object data);

        Task<IEnumerable<object>> Emit(object action);

        Task<object> EmitOne(string key, object data);

        Task<object> EmitOne(object action);

        Task End(string key, object data);

        Task End(object action);
    }
}
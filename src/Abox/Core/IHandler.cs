using System.Threading.Tasks;

namespace Abox.Core
{
    public interface IHandler
    {
        Task Execute(object message, IContext context);
    }
}
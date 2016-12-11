using Abox.Auth.Models;

namespace Abox.Data.Models
{
    public class Collection<TDocument>
    {
        public virtual bool IsAuthorized(Authorization authorization)
        {
            return true;
        }
    }
}
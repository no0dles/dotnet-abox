using Abox.Auth.Models;

namespace Abox.Data.Models
{
    public class Collection<TDocument>
    {
        public virtual DataPermission Create { get; }
        public virtual DataPermission Update { get; }
        public virtual DataPermission Read { get; }
        public virtual DataPermission Delete { get; }

        public virtual bool IsAuthorized(Authorization authorization)
        {
            return true;
        }
    }
}
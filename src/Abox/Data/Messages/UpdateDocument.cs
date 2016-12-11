using Abox.Auth.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [AuthorizeAnonymous]
    public class UpdateDocument<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
using Abox.Auth.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [AuthorizeAnonymous]
    public class DeleteDocument<TDocument>
        where TDocument : Document
    {
        public string Id { get; set; }
    }
}
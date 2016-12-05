using Abox.Core.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.delete.document")]
    public class DeleteDocument<TDocument>
        where TDocument : Document
    {
        public string Id { get; set; }
    }
}
using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.delete.document.result")]
    [Internal]
    public class DeleteDocumentResult<TDocument>
        where TDocument : Document
    {
        public string Id { get; set; }
    }
}
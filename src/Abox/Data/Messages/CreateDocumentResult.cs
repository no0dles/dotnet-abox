using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.created.document.result")]
    [Internal(true)]
    public class CreateDocumentResult<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
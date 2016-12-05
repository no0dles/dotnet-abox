using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.update.document.result")]
    [Internal]
    public class UpdateDocumentResult<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
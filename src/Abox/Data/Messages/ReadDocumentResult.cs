using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.read.document.result")]
    [Internal]
    public class ReadDocumentResult<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
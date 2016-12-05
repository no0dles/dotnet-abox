using Abox.Core.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.create.document")]
    public class CreateDocument<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
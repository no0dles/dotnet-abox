using Abox.Core.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.update.document")]
    public class UpdateDocument<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
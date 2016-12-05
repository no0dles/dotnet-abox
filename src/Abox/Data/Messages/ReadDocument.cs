using Abox.Core.Attributes;
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    [Message("data.read.document")]
    public class ReadDocument<TDocument>
        where TDocument : Document
    {
        public string Id { get; set; }
    }
}
using Abox.Data.Models;

namespace Abox.Data.Messages
{
    public class UpdateDocument<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
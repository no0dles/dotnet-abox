using Abox.Data.Models;

namespace Abox.Data.Messages
{
    public class CreateDocument<TDocument>
        where TDocument : Document
    {
        public TDocument Document { get; set; }
    }
}
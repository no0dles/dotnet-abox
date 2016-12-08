using Abox.Data.Models;

namespace Abox.Data.Messages
{
    public class DeleteDocument<TDocument>
        where TDocument : Document
    {
        public string Id { get; set; }
    }
}
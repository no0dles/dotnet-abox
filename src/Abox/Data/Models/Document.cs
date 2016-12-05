using System;

namespace Abox.Data.Models
{
    public class Document
    {
        public string Id { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}
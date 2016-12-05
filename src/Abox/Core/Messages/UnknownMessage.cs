using Abox.Core.Attributes;
using Abox.Security.Attributes;

namespace Abox.Core.Messages
{
    [Message("abox.unknown")]
    [Internal]
    public class UnknownMessage
    {
        public string Key { get; set; }
    }
}
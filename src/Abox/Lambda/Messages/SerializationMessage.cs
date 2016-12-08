using System.Collections.Generic;
using Abox.Auth.Attributes;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Newtonsoft.Json.Linq;

namespace Abox.Lambda.Messages
{
    [Internal]
    [AuthorizeAnonymous]
    [Message("lambda.serialization")]
    public class SerializationMessage
    {
        public IEnumerable<Message<JObject>> Messages { get; set; }
    }
}
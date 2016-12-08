using Abox.Security.Attributes;
using Abox.Validation.Attributes;

namespace Abox.Validation.Messages
{
    [Internal]
    public class ValidationMessage<TAttribute>
        where TAttribute : Attributes.Validation
    {
        public TAttribute Attribute { get; set; }
        public string Property { get; set; }
        public object Value { get; set; }
    }
}
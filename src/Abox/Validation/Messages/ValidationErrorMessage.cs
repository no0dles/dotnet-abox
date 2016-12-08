using Abox.Security.Attributes;

namespace Abox.Validation.Messages
{
    [Internal]
    public class ValidationErrorMessage
    {
        public string Property { get; set; }
        public string Message { get; set; }
    }
}
using System.Collections.Generic;
using Abox.Security.Attributes;

namespace Abox.Validation.Messages
{
    [Internal(true)]
    public class ValidationResultMessage
    {
        public IEnumerable<ValidationErrorMessage> Errors { get; set; }
    }
}
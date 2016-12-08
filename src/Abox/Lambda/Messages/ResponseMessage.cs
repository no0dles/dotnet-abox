using Abox.Auth.Attributes;
using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Amazon.Lambda.APIGatewayEvents;

namespace Abox.Lambda.Messages
{
    [Message("lambda.response")]
    [Internal]
    [AuthorizeAnonymous]
    public class ResponseMessage
    {
        public APIGatewayProxyResponse Response { get; set; }
    }
}
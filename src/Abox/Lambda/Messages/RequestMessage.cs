using Abox.Auth.Attributes;
using Abox.Core.Attributes;
using Abox.Security.Attributes;
using Amazon.Lambda.APIGatewayEvents;

namespace Abox.Lambda.Messages
{
    [Internal]
    [AuthorizeAnonymous]
    [Message("lambda.request")]
    public class RequestMessage
    {
        public APIGatewayProxyRequest Request { get; set; }
    }
}
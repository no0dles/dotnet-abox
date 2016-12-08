using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Lambda.Messages;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Abox.Lambda.Handlers
{
    public class RequestHandler : Handler<RequestMessage>
    {
        public override async Task Run(RequestMessage message, IContext context)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "",
                Headers = new Dictionary<string, string> {
                    { "Content-Type", "application/json" }
                }
            };

            var messages = JsonConvert.DeserializeObject<List<Message<JObject>>>(message.Request?.Body);

            var responseMessages = await context.Emit(new SerializationMessage
            {
                Messages = messages
            });

            response.Body = JsonConvert.SerializeObject(responseMessages);

            //Console.WriteLine("body: " + JsonConvert.SerializeObject(response.Body));

            await context.Emit(new ResponseMessage
            {
                Response = response
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Abox.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Abox.Lambda
{
    public static class LambdaRunner
    {
        public static async Task<APIGatewayProxyResponse> Run<TModule>(APIGatewayProxyRequest request, ILambdaContext context)
            where TModule : Module, new()
        {
            var module = new TModule();

            module.Configure();

            module.Dependencies.AddSingleton(() => context);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "",
                Headers = new Dictionary<string, string> {
                    { "Content-Type", "application/json" }
                }
            };

            try
            {
                var messages = JsonConvert.DeserializeObject<List<Message<JObject>>>(request?.Body);

                if(messages == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return response;
                }

                var convertedMessages = messages
                    .Select(message => new Message<object>
                    {
                        Key = message.Key,
                        Value = message.Value.ToObject(module.Messages[message.Key])
                    });

                response.Body = JsonConvert.SerializeObject(await module.Execute(convertedMessages));
            }
            catch(Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Body = JsonConvert.SerializeObject(new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }

            return response;
        }
    }
}
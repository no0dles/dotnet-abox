using System;
using System.Threading.Tasks;
using System.Linq;
using Abox.Core;
using Abox.Lambda.Handlers;
using Abox.Lambda.Messages;
using Abox.Lambda.Models;
using Abox.Lambda.Services;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace Abox.Lambda
{
    public static class LambdaRunner
    {
        public static async Task<APIGatewayProxyResponse> Run<TModule>(APIGatewayProxyRequest request, ILambdaContext context)
            where TModule : Module, new()
        {
            var module = new TModule();
            var serializationService = new SerializationService();
            var builder = new LambdaModuleBuilder(module, serializationService);

            builder.AddHandler<RequestHandler, RequestMessage>();
            builder.AddHandler<SerializationHandler, SerializationMessage>();

            module.Configure(builder);

            module.Dependencies.AddSingleton(() => context);
            module.Dependencies.AddSingleton(() => serializationService);

            var handlerContext = new Context(module);
            var responses = await handlerContext.Emit(new RequestMessage {Request = request});

            return responses.OfType<ResponseMessage>()
                .FirstOrDefault()
                ?.Response;
        }
    }
}
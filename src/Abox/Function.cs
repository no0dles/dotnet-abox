using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Auth;
using Abox.Auth.Attributes;
using Abox.Data;
using Abox.Data.Extensions;
using Abox.Data.Messages;
using Abox.Data.Models;
using Abox.Lambda;

[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Abox
{

    public class Todo : Document
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    
    [AuthorizeRoles("admin")]
    [Message("create.todo")]
    public class CreateTodo : CreateDocument<Todo>
    {

    }

    public class DemoModule : Module
    {
        public override void Configure(IModuleBuilder builder)
        {
            builder.Use<DataModule>();
            builder.Use<AuthModule>();

            builder.AddDocument<Todo>();
        }
    }


    public class Function
    {
        public Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            return LambdaRunner.Run<DemoModule>(request, context);
        }
    }
}

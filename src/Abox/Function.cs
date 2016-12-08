using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Abox.Core;
using Abox.Auth;
using Abox.Data;
using Abox.Data.Attributes;
using Abox.Data.Extensions;
using Abox.Data.Models;
using Abox.Lambda;
using Abox.Validation;
using Abox.Validation.Attributes;

[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Abox
{
    [Collection("todo")]
    public class TodoCollection : Collection<Todo>
    {
        public override DataPermission Create => new DataPermission
        {
            Anonymous = true,
            Roles = {"user", "admin"}
        };

        public override DataPermission Update => new DataPermission
        {
            Owner = true,
            Roles = {"admin"}
        };

        public override DataPermission Delete => new DataPermission
        {
            Owner = true,
            Roles = {"admin"}
        };

        public override DataPermission Read => new DataPermission
        {
            Owner = true,
            Roles = {"admin"}
        };
    }

    public class Todo : Document
    {
        [Length(min: 3, max: 30)]
        [Pattern("^[a-zA-Z]+$")]
        public string Title { get; set; }

        [Length(min: 0, max: 500)]
        public string Description { get; set; }

        [Owner]
        public string Owner { get; set; }
    }

    public class DemoModule : Module
    {
        public override void Configure(IModuleBuilder builder)
        {
            builder.AddModule<ValidationModule>();
            builder.AddModule<DataModule>();
            builder.AddModule<AuthModule>();

            builder.AddCollection<TodoCollection, Todo>();
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

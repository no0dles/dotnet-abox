using System;
using System.Linq;
using Abox.Auth.Models;
using Abox.Core.Tests;
using Abox.Data.Attributes;
using Abox.Data.Messages;
using Abox.Data.Models;
using Xunit;

namespace Abox.Data.Handlers
{
    [RoleReadWrite("admin")]
    public class TestDocument : Document
    {
        public string Message { get; set; }
    }


    public class AuthorizationHandlerTest
    {
        [Fact]
        public async void TestUnauthorizedMessage()
        {
            var handler = new AuthorizationHandler();
            var context = new MockContext();

            handler.Auth = new Authorization();

            await handler.Run(new CreateDocument<TestDocument>
            {
                Document = new TestDocument()
            }, context);

            var unauthorized = context.Messages
                .Select(m => m.Value)
                .OfType<Unauthorized>();

            Assert.NotEmpty(unauthorized);
        }

        [Fact]
        public async void TestAuthorizedMessage()
        {
            var handler = new AuthorizationHandler();
            var context = new MockContext();

            handler.Auth = new Authorization();
            handler.Auth.Roles.Add("admin");

            await handler.Run(new CreateDocument<TestDocument>
            {
                Document = new TestDocument()
            }, context);

            var unauthorized = context.Messages
                .Select(m => m.Value)
                .OfType<Unauthorized>();

            Assert.Empty(unauthorized);
        }
    }
}
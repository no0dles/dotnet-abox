using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abox.Core;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Abox.Auth.Messages;
using Abox.Auth.Models;
using Abox.Data.Messages;
using Newtonsoft.Json;


namespace Abox.Tests
{
    public class FunctionTest
    {
        string TableName { get; }
        IAmazonDynamoDB DDBClient { get; }

        public FunctionTest()
        {
            TableName = "DynamoDBBlogAPI-Blogs-" + DateTime.Now.Ticks;
            DDBClient = new AmazonDynamoDBClient(RegionEndpoint.EUWest1);

            //SetupTableAsync().Wait();
        }

        [Fact]
        public async void TestToUpperFunction()
        {
            var function = new Function();
            var action = new List<Message<object>>();

            action.Add(new Message<object>
            {
                Key = "auth.token",
                Value = new TokenMessage
                {
                    Auth = new Authorization
                    {
                        Roles = {"admin"}
                    }
                }
            });

            action.Add(new Message<object>
            {
                Key = "data.create.todo",
                Value = new CreateDocument<Todo>
                {
                    Document = new Todo
                    {
                        Title = "t",
                        Description = "this is a test"
                    }
                }
            });

            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();

            request.Body = JsonConvert.SerializeObject(action);

            var response = await function.FunctionHandler(request, context);

            Assert.Equal(response?.StatusCode, 200);
        }

        private async Task SetupTableAsync()
        {   
            var request = new CreateTableRequest
            {
                TableName = this.TableName,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 2
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        KeyType = KeyType.HASH,
                        AttributeName = "Id"
                    }
                },
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = ScalarAttributeType.S
                    }
                }
            };

            await this.DDBClient.CreateTableAsync(request);

            var describeRequest = new DescribeTableRequest { TableName = this.TableName };
            DescribeTableResponse response = null;
            do
            {
                Thread.Sleep(1000);
                response = await this.DDBClient.DescribeTableAsync(describeRequest);
            } while (response.Table.TableStatus != TableStatus.ACTIVE);
        }

        /*

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.DDBClient.DeleteTableAsync(this.TableName).Wait();
                    this.DDBClient.Dispose();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    */
    }
}

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Abox.Data.Services
{
    public class DataService
    {
        public DynamoDBContextConfig Config { get; }
        public DynamoDBContext Context { get; }

        public DataService()
        {
            AWSConfigsDynamoDB.Context.TypeMappings[typeof(Todo)] = new Amazon.Util.TypeMapping(typeof(Todo), "todo");

            Config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            Context = new DynamoDBContext(new AmazonDynamoDBClient(RegionEndpoint.EUWest1), Config);
        }
    }
}
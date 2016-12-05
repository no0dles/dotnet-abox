using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Data.Messages;
using Abox.Data.Models;
using Abox.Data.Services;

namespace Abox.Data.Handlers
{
    public class ReadDocumentHandler<TDocument> : Handler<ReadDocument<TDocument>>
        where TDocument : Document
    {
        [Inject]
        public DataService Data { get; set; }

        public override async Task Run(ReadDocument<TDocument> message, IContext context)
        {
            var document = await Data.Context.LoadAsync<TDocument>(message.Id);

            await context.Emit(new ReadDocumentResult<TDocument> { 
                Document = document
            });
        }
    }
}
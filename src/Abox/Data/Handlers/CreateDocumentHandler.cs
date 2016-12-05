using System;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Data.Messages;
using Abox.Data.Models;
using Abox.Data.Services;

namespace Abox.Data.Handlers
{
    public class CreateDocumentHandler<TDocument> : Handler<CreateDocument<TDocument>>
        where TDocument : Document
    {
        [Inject]
        public DataService Data { get; set; }

        public override async Task Run(CreateDocument<TDocument> message, IContext context)
        {
            message.Document.Id = Guid.NewGuid().ToString();
            message.Document.CreatedTimestamp = DateTime.UtcNow;
            message.Document.UpdatedTimestamp = DateTime.UtcNow;

            await Data.Context.SaveAsync<TDocument>(message.Document);

            await context.Emit(new CreateDocumentResult<TDocument> { 
                Document = message.Document
            });
        }
    }
}
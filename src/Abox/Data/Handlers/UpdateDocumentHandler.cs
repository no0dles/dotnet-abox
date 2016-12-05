using System;
using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Data.Messages;
using Abox.Data.Models;
using Abox.Data.Services;

namespace Abox.Data.Handlers
{
    public class UpdateDocumentHandler<TDocument> : Handler<UpdateDocument<TDocument>>
        where TDocument : Document
    {
        [Inject]
        public DataService Data { get; set; }

        public override async Task Run(UpdateDocument<TDocument> action, IContext context)
        {
            action.Document.UpdatedTimestamp = DateTime.UtcNow;

            await Data.Context.SaveAsync<TDocument>(action.Document);

            await context.Emit(new UpdateDocumentResult<TDocument> { 
                Document = action.Document
            });
        }
    }
}
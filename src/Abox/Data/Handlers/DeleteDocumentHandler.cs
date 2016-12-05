using System.Threading.Tasks;
using Abox.Core;
using Abox.Core.Attributes;
using Abox.Data.Messages;
using Abox.Data.Models;
using Abox.Data.Services;

namespace Abox.Data.Handlers
{
    public class DeleteDocumentHandler<TDocument> : Handler<DeleteDocument<TDocument>>
        where TDocument : Document
    {
        [Inject]
        public DataService Data { get; set; }

        public override async Task Run(DeleteDocument<TDocument> action, IContext context)
        {
            await Data.Context.DeleteAsync<TDocument>(action.Id);

            await context.Emit(new DeleteDocumentResult<TDocument> { 
                Id = action.Id
            });
        }
    }
}
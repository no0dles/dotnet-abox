using Abox.Core;
using Abox.Data.Handlers;
using Abox.Data.Messages;
using Abox.Data.Models;

namespace Abox.Data.Extensions
{
    public static class ModuleBuilderExtension
    {
        public static void AddDocument<TDocument>(this IModuleBuilder builder)
            where TDocument : Document, new()
        {
            builder.AddHandler<CreateDocumentHandler<TDocument>, CreateDocument<TDocument>>();
            builder.AddHandler<UpdateDocumentHandler<TDocument>, UpdateDocument<TDocument>>();
            builder.AddHandler<DeleteDocumentHandler<TDocument>, DeleteDocument<TDocument>>();
            builder.AddHandler<ReadDocumentHandler<TDocument>, ReadDocument<TDocument>>();
        }
    }
}
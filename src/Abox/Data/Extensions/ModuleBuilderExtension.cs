using System;
using System.Reflection;
using Abox.Core;
using Abox.Data.Attributes;
using Abox.Data.Handlers;
using Abox.Data.Messages;
using Abox.Data.Models;

namespace Abox.Data.Extensions
{
    public static class ModuleBuilderExtension
    {
        public static void AddCollection<TCollection, TDocument>(this IModuleBuilder builder)
            where TCollection : Collection<TDocument>, new()
            where TDocument : Document
        {
            var collectionAttribute = typeof(TCollection)
                .GetTypeInfo()
                .GetCustomAttribute<Collection>();

            if (collectionAttribute == null)
                throw new Exception("missing collection attribute");

            AddCollection<TCollection, TDocument>(builder, collectionAttribute.Name);
        }

        public static void AddCollection<TCollection, TDocument>(this IModuleBuilder builder, string collection)
            where TCollection : Collection<TDocument>, new()
            where TDocument : Document
        {
            builder.AddHandler<CreateDocumentHandler<TDocument>, CreateDocument<TDocument>>($"data.create.{collection}");
            builder.AddHandler<UpdateDocumentHandler<TDocument>, UpdateDocument<TDocument>>($"data.update.{collection}");
            builder.AddHandler<DeleteDocumentHandler<TDocument>, DeleteDocument<TDocument>>($"data.delete.{collection}");
            builder.AddHandler<ReadDocumentHandler<TDocument>, ReadDocument<TDocument>>($"data.read.{collection}");
        }
    }
}
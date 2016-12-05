namespace Abox.Core
{
    public interface IModuleBuilder
    {
        DependencyManager Dependencies { get; }

        void AddHandler<THandler, TMessage>(string key)
            where THandler : Handler<TMessage>, new()
            where TMessage : class, new();

        void AddHandler<THandler, TMessage>()
            where THandler : Handler<TMessage>, new()
            where TMessage : class, new();

        void AddEvent<TMessage>()
            where TMessage : class, new();

        void AddEvent<TMessage>(string key)
            where TMessage : class, new();

        void Use<TModule>()
            where TModule : Module, new();
    }
}
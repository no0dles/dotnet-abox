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

        void AddModule<TModule>()
            where TModule : Module, new();
    }
}
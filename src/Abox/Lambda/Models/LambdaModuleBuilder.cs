using Abox.Core;
using Abox.Lambda.Services;

namespace Abox.Lambda.Models
{
    public class LambdaModuleBuilder : ModuleBuilder
    {
        private readonly SerializationService serializationService;

        public LambdaModuleBuilder(Module module, SerializationService serializationService) : base(module)
        {
            this.serializationService = serializationService;
        }

        public override void AddHandler<THandler, TMessage>(string key)
        {
            base.AddHandler<THandler, TMessage>(key);

            serializationService.Register<TMessage>(key);
        }
    }
}
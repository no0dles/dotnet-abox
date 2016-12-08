using System.Collections.Generic;
using System.Linq;

namespace Abox.Core
{
    public abstract class Module
    {
        public DependencyManager Dependencies { get; }
        public List<Handle> Handles { get; }

        protected Module()
        {
            Handles = new List<Handle>();

            Dependencies = new DependencyManager();
            Dependencies.AddSingleton(() => Dependencies);
        }

        public abstract void Configure(IModuleBuilder builder);

        public void Configure()
        {
            Configure(new ModuleBuilder(this));
        }

        public IEnumerable<Handle> Resolve(string key)
        {
            return Handles.Where(h => h.Matches(key));
        }
    }
}
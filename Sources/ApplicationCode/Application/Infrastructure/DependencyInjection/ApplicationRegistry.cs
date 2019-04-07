using Mmu.Rb.Application.Areas.Testing.Services;
using Mmu.Rb.Application.Areas.Testing.Services.Implementation;
using Mmu.Rb.Application.Areas.Testing.Services.Servants;
using Mmu.Rb.Application.Areas.Testing.Services.Servants.Implementation;
using StructureMap;

namespace Mmu.Rb.Application.Infrastructure.DependencyInjection
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType(typeof(ApplicationRegistry));
                    scanner.WithDefaultConventions();
                });

            For<IModelTestInitializationService>().Use<ModelTestInitializationService>().Singleton();
            For<IModelClassInfoFactory>().Use<ModelClassInfoFactory>().Singleton();
            For<IModelTestClassFactory>().Use<ModelTestClassFactory>().Singleton();
        }
    }
}
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants
{
    public interface IModelTestClassFactory
    {
        ModelTestClass Create(ModelClassInfo modelClassInfo);
    }
}
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants
{
    public interface IModelClassInfoFactory
    {
        ModelClassInfo CreateFromFile(string filePath);
    }
}
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants
{
    public interface IModelClassInfoFactory
    {
        FunctionResult<ModelClassInfo> TryCreatingFromFile(string filePath);
    }
}
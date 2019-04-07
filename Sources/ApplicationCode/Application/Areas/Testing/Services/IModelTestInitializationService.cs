using System.Threading.Tasks;

namespace Mmu.Rb.Application.Areas.Testing.Services
{
    public interface IModelTestInitializationService
    {
        Task InitializeAllAsync(string folderPath);
    }
}
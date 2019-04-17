using System.Threading.Tasks;
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services
{
    public interface IModelTestInitializationService
    {
        Task InitializeAllAsync(ModelInitializationParameters initParams);
    }
}
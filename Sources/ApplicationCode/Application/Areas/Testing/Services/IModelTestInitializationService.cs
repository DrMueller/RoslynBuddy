using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmu.Rb.Application.Areas.Testing.Services
{
    public interface IModelTestInitializationService
    {
        Task InitializeAllAsync(string filePath);
    }
}

using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Rb.Application.Areas.Testing.Models;
using Mmu.Rb.Application.Areas.Testing.Services.Servants;

namespace Mmu.Rb.Application.Areas.Testing.Services.Implementation
{
    public class ModelTestInitializationService : IModelTestInitializationService
    {
        private readonly IFileSystem _fileSystem;
        private readonly IModelClassInfoFactory _modelClassInfoFactory;
        private readonly IModelTestClassFactory _modelTestClassFactory;

        public ModelTestInitializationService(
            IFileSystem fileSystem,
            IModelClassInfoFactory modelClassInfoFactory,
            IModelTestClassFactory modelTestClassFactory)
        {
            _fileSystem = fileSystem;
            _modelClassInfoFactory = modelClassInfoFactory;
            _modelTestClassFactory = modelTestClassFactory;
        }

        public Task InitializeAllAsync(ModelInitializationParameters initParams)
        {
            var csharpFilePaths = _fileSystem.Directory.GetFiles(initParams.FolderPath, "*.cs").ToList();

            var generatedDirectory = _fileSystem.Path.Combine(initParams.FolderPath, "Generated");
            _fileSystem.Directory.CreateDirectory(generatedDirectory);

            foreach (var filePath in csharpFilePaths)
            {
                var modelClassInfoResult = _modelClassInfoFactory.TryCreatingFromFile(filePath);
                if (modelClassInfoResult.IsSuccess)
                {
                    var testClass = _modelTestClassFactory.Create(modelClassInfoResult.Value, initParams.TestAssemblyBaseNamespace);
                    var fullTestPath = _fileSystem.Path.Combine(generatedDirectory, testClass.FileName);
                    _fileSystem.File.WriteAllText(fullTestPath, testClass.FileContent);
                }
            }

            return Task.CompletedTask;
        }
    }
}
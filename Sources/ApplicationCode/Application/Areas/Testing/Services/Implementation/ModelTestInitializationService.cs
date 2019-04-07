using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
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

        public Task InitializeAllAsync(string folderPath)
        {
            var csharpFiles = _fileSystem.Directory.GetFiles(folderPath, "*.cs").ToList();

            foreach (var file in csharpFiles)
            {
                var modelClassInfo = _modelClassInfoFactory.CreateFromFile(file);
                var testClass = _modelTestClassFactory.Create(modelClassInfo);
                var fileName = _fileSystem.Path.GetFileName(file);
                var fullTestPath = file.Replace(fileName, testClass.FileName);
                _fileSystem.File.WriteAllText(fullTestPath, testClass.FileContent);
            }

            return Task.CompletedTask;
        }
    }
}
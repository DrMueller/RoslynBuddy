using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Rb.Application.Areas.Testing.Models
{
    public class ModelInitializationParameters
    {
        public string FolderPath { get; }
        public string TestAssemblyBaseNamespace { get; }

        public ModelInitializationParameters(string folderPath, string testAssemblyBaseNamespace)
        {
            Guard.StringNotNullOrEmpty(() => folderPath);
            Guard.StringNotNullOrEmpty(() => testAssemblyBaseNamespace);

            FolderPath = folderPath;
            TestAssemblyBaseNamespace = testAssemblyBaseNamespace;
        }
    }
}
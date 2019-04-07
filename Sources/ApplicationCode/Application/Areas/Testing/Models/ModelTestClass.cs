using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Rb.Application.Areas.Testing.Models
{
    public class ModelTestClass
    {
        public string FileContent { get; }
        public string FileName { get; }

        public ModelTestClass(string fileName, string fileContent)
        {
            Guard.StringNotNullOrEmpty(() => fileName);
            Guard.StringNotNullOrEmpty(() => fileContent);

            FileName = fileName;
            FileContent = fileContent;
        }
    }
}
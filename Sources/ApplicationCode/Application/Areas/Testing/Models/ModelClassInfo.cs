using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Rb.Application.Areas.Testing.Models
{
    public class ModelClassInfo
    {
        public string ClassName { get; }
        public string NamespaceDecl { get; }
        public IReadOnlyCollection<Constructor> Constructors { get; }

        public ModelClassInfo(string className, string namespaceDecl, IReadOnlyCollection<Constructor> constructors)
        {
            Guard.StringNotNullOrEmpty(() => className);
            Guard.StringNotNullOrEmpty(() => namespaceDecl);
            Guard.ObjectNotNull(() => constructors);

            ClassName = className;
            NamespaceDecl = namespaceDecl;
            Constructors = constructors;
        }
    }
}
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Rb.Application.Areas.Testing.Models
{
    public class ModelClassInfo
    {
        public string ClassName { get; }
        public IReadOnlyCollection<Constructor> Constructors { get; }

        public ModelClassInfo(string className, IReadOnlyCollection<Constructor> constructors)
        {
            Guard.StringNotNullOrEmpty(() => className);
            Guard.ObjectNotNull(() => constructors);

            ClassName = className;
            Constructors = constructors;
        }
    }
}
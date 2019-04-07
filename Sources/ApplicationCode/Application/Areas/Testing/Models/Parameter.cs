using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Rb.Application.Areas.Testing.Models
{
    public class Parameter
    {
        public string ParameterName { get; }
        public string ParameterType { get; }

        public Parameter(string parameterType, string parameterName)
        {
            Guard.StringNotNullOrEmpty(() => parameterType);
            Guard.StringNotNullOrEmpty(() => parameterName);

            ParameterType = parameterType;
            ParameterName = parameterName;
        }
    }
}
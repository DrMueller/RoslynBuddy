using System.IO.Abstractions;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants.Implementation
{
    internal class ModelClassInfoFactory : IModelClassInfoFactory
    {
        private readonly IFileSystem _fileSystem;

        public ModelClassInfoFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public FunctionResult<ModelClassInfo> TryCreatingFromFile(string filePath)
        {
            var fileContent = _fileSystem.File.ReadAllText(filePath);
            var tree = CSharpSyntaxTree.ParseText(fileContent);
            var root = tree.GetRoot();

            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();

            if (classDeclaration == null)
            {
                return FunctionResult.CreateFailure<ModelClassInfo>();
            }

            var fullNamespace = root
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>().First()
                .Name
                .ToString();
            var className = classDeclaration.Identifier.Text;

            var ctorDeclarations = root.DescendantNodes().OfType<ConstructorDeclarationSyntax>();
            var ctors = ctorDeclarations.Select(
                ctorDecl =>
                {
                    var ctorParams = ctorDecl.DescendantNodes()
                        .OfType<PredefinedTypeSyntax>()
                        .Select(f => new Parameter(f.Keyword.ToString(), f.Keyword.ToString()))
                        .ToList();

                    return new Constructor(ctorParams);
                }).ToList();

            var classInfo = new ModelClassInfo(className, fullNamespace, ctors);
            return FunctionResult.CreateSuccess(classInfo);
        }
    }
}
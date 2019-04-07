using System.IO.Abstractions;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public ModelClassInfo CreateFromFile(string filePath)
        {
            var fileContent = _fileSystem.File.ReadAllText(filePath);
            var tree = CSharpSyntaxTree.ParseText(fileContent);
            var root = tree.GetRoot();

            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
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

            return new ModelClassInfo(className, ctors);
        }
    }
}
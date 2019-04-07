using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants.Implementation
{
    internal class ModelTestClassFactory : IModelTestClassFactory
    {
        public ModelTestClass Create(ModelClassInfo modelClassInfo)
        {
            var ns = CreateNamespace();
            var cd = InitializeClass(modelClassInfo.ClassName);
            cd = AppendSutField(cd, modelClassInfo.ClassName);
            cd = AppendSetUpMethod(cd, modelClassInfo.ClassName);
            cd = AppendConstructorTestMethod(cd);
            ns = ns.AddMembers(cd);

            var fileContent = ns
                .NormalizeWhitespace()
                .ToFullString();

            var testFileName = $"{modelClassInfo.ClassName}UnitTests.cs";
            return new ModelTestClass(testFileName, fileContent);
        }

        private static ClassDeclarationSyntax AppendConstructorTestMethod(ClassDeclarationSyntax cd)
        {
            var statement1 = SyntaxFactory.ParseStatement(
                @"ConstructorTestBuilderFactory.Constructing<FortrasOrder>()
                .UsingDefaultConstructor();");

            return cd.AddMembers(
                    SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Constructor_Works")
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                        .WithBody(SyntaxFactory.Block(statement1)))
                .AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Test")))));
        }

        private static ClassDeclarationSyntax AppendSetUpMethod(ClassDeclarationSyntax cd, string classTypeName)
        {
            var syntax = SyntaxFactory.ParseStatement($"_sut = new {classTypeName}();");

            return cd.AddMembers(
                SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Align")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .WithBody(SyntaxFactory.Block(syntax))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("SetUp"))))));
        }

        private static ClassDeclarationSyntax AppendSutField(ClassDeclarationSyntax cd, string className)
        {
            var variableDeclaration = SyntaxFactory
                .VariableDeclaration(SyntaxFactory.ParseTypeName(className))
                .AddVariables(SyntaxFactory.VariableDeclarator("_sut"));

            var sutField = SyntaxFactory.FieldDeclaration(variableDeclaration)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            return cd.AddMembers(sutField);
        }

        private static NamespaceDeclarationSyntax CreateNamespace()
        {
            const string DefaultNamespace = "TODO";
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(DefaultNamespace)).NormalizeWhitespace();
            ns = ns.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            return ns;
        }

        private static ClassDeclarationSyntax InitializeClass(string className)
        {
            var classDeclaration = SyntaxFactory.ClassDeclaration(className + "UnitTests");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            classDeclaration = classDeclaration.AddAttributeLists(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestFixture")))));

            return classDeclaration;
        }
    }
}
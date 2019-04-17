using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mmu.Rb.Application.Areas.Testing.Models;

namespace Mmu.Rb.Application.Areas.Testing.Services.Servants.Implementation
{
    internal class ModelTestClassFactory : IModelTestClassFactory
    {
        public ModelTestClass Create(ModelClassInfo modelClassInfo, string testAssemblyBaseNamespace)
        {
            var ns = CreateNamespace(modelClassInfo.NamespaceDecl, testAssemblyBaseNamespace);
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

            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Constructor_Works")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statement1))
                .AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Test")))));

            return cd.AddMembers(method);
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

        private static NamespaceDeclarationSyntax CreateNamespace(string modelNamespace, string testAssemblyBaseNamespace)
        {
            var splittedTestNamespace = testAssemblyBaseNamespace.Split('.').ToList();
            var splittedModelNamespace = modelNamespace.Split('.').ToList();

            var newRelativeNamespace = splittedModelNamespace.ToList();

            for (var i = splittedModelNamespace.Count - 1; i >= 0; i--)
            {
                var testNamespacePart = splittedTestNamespace.ElementAtOrDefault(i);
                var modelNamespacePart = splittedModelNamespace.ElementAtOrDefault(i);

                if (string.IsNullOrEmpty(testNamespacePart) || string.IsNullOrEmpty(modelNamespacePart))
                {
                    continue;
                }

                if (string.Equals(testNamespacePart, modelNamespacePart, StringComparison.OrdinalIgnoreCase))
                {
                    newRelativeNamespace.RemoveAt(i);
                }
            }

            var newNamespace = testAssemblyBaseNamespace + "." + string.Join(".", newRelativeNamespace);
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(newNamespace)).NormalizeWhitespace();
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
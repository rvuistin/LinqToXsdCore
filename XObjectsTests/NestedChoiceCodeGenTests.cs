using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using NUnit.Framework;

namespace Xml.Schema.Linq.Tests
{
    public class NestedChoiceCodeGenTests : BaseTester
    {
        public MockFileSystem TestFiles { get; set; }
        private SyntaxTree Tree { get; set; }
        private List<ClassDeclarationSyntax> GeneratedTypes { get; set; }

        [SetUp]
        public void GenerateCode()
        {
            const string XsdFilePath = @"NestedChoiceTest\NestedChoiceTest.xsd";
            TestFiles = Utilities.GetAssemblyFileSystem(typeof(LinqToXsd.Schemas.Test.NestedChoiceTypes.NestedChoiceType).Assembly);
            Tree = Utilities.GenerateSyntaxTree(XsdFilePath, TestFiles);

            var diags = Utilities.GetSyntaxAndCompilationDiagnostics(Tree);
            //Assert.AreEqual(0, diags.Length);
            if (diags.Length > 0) {
                Assert.Warn("Diagnostics for this test class's Tree should be 0");
            }

            var nodes = Tree.GetNamespaceRoot().DescendantNodes();
            GeneratedTypes = nodes.OfType<ClassDeclarationSyntax>().ToList();
        }

        [Test]
        public void T1_NestedChoiceShouldHaveNullableValueReturnType()
        {
            var type   = GeneratedTypes.Single(type => type.Identifier.Text == "NestedChoiceType");
            var prop1 = type.Members.OfType<PropertyDeclarationSyntax>().SingleOrDefault(prop => prop.Identifier.Text == "Prop1");
            var prop2 = type.Members.OfType<PropertyDeclarationSyntax>().SingleOrDefault(prop => prop.Identifier.Text == "Prop2");
            var choice1Prop = type.Members.OfType<PropertyDeclarationSyntax>().SingleOrDefault(prop => prop.Identifier.Text == "Choice1Prop");
            var choice2Prop1 = type.Members.OfType<PropertyDeclarationSyntax>().SingleOrDefault(prop => prop.Identifier.Text == "Choice2Prop1");
            var choice2Prop2 = type.Members.OfType<PropertyDeclarationSyntax>().SingleOrDefault(prop => prop.Identifier.Text == "Choice2Prop2");
            Assert.IsTrue(prop1.Type is PredefinedTypeSyntax);
            Assert.IsTrue(prop2.Type is NullableTypeSyntax);
            Assert.IsTrue(choice1Prop.Type is NullableTypeSyntax);
            Assert.IsTrue(choice2Prop1.Type is NullableTypeSyntax);
            Assert.IsTrue(choice2Prop2.Type is NullableTypeSyntax);
        }
    }
}
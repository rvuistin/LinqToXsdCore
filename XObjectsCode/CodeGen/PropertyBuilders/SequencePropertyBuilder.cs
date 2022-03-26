//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;

namespace Xml.Schema.Linq.CodeGen
{
    internal class SequencePropertyBuilder : ContentModelPropertyBuilder
    {
        public SequencePropertyBuilder(ContentModelPropertyBuilder parentBuilder, GroupingInfo grouping, CodeTypeDeclaration decl, CodeTypeDeclItems declItems,
            GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public) :
            base(parentBuilder, grouping, decl, declItems, visibility)
        {
        }

        public override CodeObjectCreateExpression CreateContentModelExpression()
        {
            return new CodeObjectCreateExpression(new CodeTypeReference(Constants.SequenceContentModelEntity));
        }
    }
}
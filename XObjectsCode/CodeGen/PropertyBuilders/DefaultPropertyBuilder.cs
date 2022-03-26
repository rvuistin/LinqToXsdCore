//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;

namespace Xml.Schema.Linq.CodeGen
{
    internal class DefaultPropertyBuilder : TypePropertyBuilder
    {
        internal DefaultPropertyBuilder(CodeTypeDeclaration decl, CodeTypeDeclItems declItems, 
            GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public) : base(decl, declItems, visibility)
        {
        }
    }
}
//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Xml.Schema.Linq.CodeGen
{
    internal class SymbolEntry
    {
        public string xsdNamespace;
        public string clrNamespace;
        public string symbolName; //schema-name
        public string identifierName; //clr-name

        public override int GetHashCode()
        {
            return identifierName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            SymbolEntry se = obj as SymbolEntry;
            if (se != null)
            {
                return (xsdNamespace == se.xsdNamespace) &&
                       identifierName.Equals(se.identifierName, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public bool isNameFixed()
        {
            return symbolName != identifierName;
        }
    }
}
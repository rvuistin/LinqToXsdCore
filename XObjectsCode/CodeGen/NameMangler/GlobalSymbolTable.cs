//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Globalization;

namespace Xml.Schema.Linq.CodeGen
{
    internal class GlobalSymbolTable
    {
        internal Dictionary<SymbolEntry, SymbolEntry> symbols;
        internal Dictionary<XmlSchemaObject, string> schemaNameToIdentifiers;
        internal int nFixedNames = 0;
        LinqToXsdSettings configSettings;

        public GlobalSymbolTable(LinqToXsdSettings settings)
        {
            configSettings = settings;
            symbols = new Dictionary<SymbolEntry, SymbolEntry>();
            schemaNameToIdentifiers = new Dictionary<XmlSchemaObject, string>();
        }

        public SymbolEntry AddElement(XmlSchemaElement element)
        {
            return AddSymbol(element.QualifiedName, element, string.Empty);
        }

        public SymbolEntry AddType(XmlQualifiedName qualifiedName, XmlSchemaType type)
        {
            return AddSymbol(qualifiedName, type, Constants.TypeSuffix);
        }

        protected SymbolEntry AddSymbol(XmlQualifiedName qname, XmlSchemaObject schemaObject, string suffix)
        {
            SymbolEntry symbol = new SymbolEntry();
            symbol.xsdNamespace = qname.Namespace;
            symbol.clrNamespace = configSettings.GetClrNamespace(qname.Namespace);
            symbol.symbolName = qname.Name;
            string identifierName = NameGenerator.MakeValidIdentifier(symbol.symbolName);
            symbol.identifierName = identifierName;
            int id = 0;
            if (symbols.ContainsKey(symbol))
            {
                identifierName = identifierName + suffix;
                symbol.identifierName = identifierName;
                while (symbols.ContainsKey(symbol))
                {
                    id++;
                    symbol.identifierName = identifierName + id.ToString(CultureInfo.InvariantCulture.NumberFormat);
                }
            }

            if (symbol.isNameFixed())
                nFixedNames++;

            symbols.Add(symbol, symbol);
            schemaNameToIdentifiers.Add(schemaObject, symbol.identifierName); //Type vs typeName
            return symbol;
        }
    }
}
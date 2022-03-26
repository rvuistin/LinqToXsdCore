//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Xml.Schema.Linq.CodeGen
{
    internal class LocalSymbolTable
    {
        Hashtable symbolToQName;
        Hashtable qNameToSymbol;
        List<AnonymousType> anonymousTypes;

        public void Init(XmlSchemaElement element)
        {
            Init(element.QualifiedName.Name);
        }

        public void Init(XmlSchemaType type)
        {
            Init(type.QualifiedName.Name);
        }

        public void Init(string className)
        {
            if (anonymousTypes == null)
            {
                symbolToQName = new Hashtable();
                qNameToSymbol = new Hashtable();
                anonymousTypes = new List<AnonymousType>();
            }
            else
                Reset();

            symbolToQName.Add(className.ToUpper(CultureInfo.InvariantCulture), XmlQualifiedName.Empty);
        }

        public void Reset()
        {
            symbolToQName.Clear();
            qNameToSymbol.Clear();
            if (anonymousTypes.Count > 0)
                anonymousTypes = new List<AnonymousType>();
        }

        public string AddLocalElement(XmlSchemaElement element)
        {
            string identifierName = (string) qNameToSymbol[element.QualifiedName];
            if (identifierName != null)
                return identifierName;
            identifierName = NameGenerator.MakeValidIdentifier(element.QualifiedName.Name);
            identifierName = getSymbol(identifierName, Constants.LocalElementConflictSuffix);
            symbolToQName.Add(identifierName.ToUpper(CultureInfo.InvariantCulture), element.QualifiedName);
            qNameToSymbol.Add(element.QualifiedName, identifierName);
            return identifierName;
        }

        public string AddAttribute(XmlSchemaAttribute attribute)
        {
            string identifierName = NameGenerator.MakeValidIdentifier(attribute.QualifiedName.Name);
            identifierName = getSymbol(identifierName, Constants.LocalAttributeConflictSuffix);
            symbolToQName.Add(identifierName.ToUpper(CultureInfo.InvariantCulture), attribute.QualifiedName);
            return identifierName;
        }

        public void AddAnonymousType(string identifier, XmlSchemaElement parentElement,
            ClrTypeReference parentElementTypeRef)
        {
            AnonymousType at = new AnonymousType();
            at.identifier = identifier;
            at.typeRefence = parentElementTypeRef;
            at.parentElement = parentElement;
            anonymousTypes.Add(at);
        }

        public void AddComplexRestrictedContentType(XmlSchemaComplexType wrappingType, ClrTypeReference wrappingTypeRef)
        {
            string identifier = NameGenerator.MakeValidIdentifier(wrappingType.Name);
            AnonymousType at = new AnonymousType();
            at.identifier = identifier;
            at.typeRefence = wrappingTypeRef;
            at.wrappingType = wrappingType;
            anonymousTypes.Add(at);
        }

        public List<AnonymousType> GetAnonymousTypes()
        {
            foreach (AnonymousType at in anonymousTypes)
            {
                ClrTypeReference typeReference = at.typeRefence;
                string typeIdentifier = getSymbol(at.identifier, at.typeRefence.LocalSuffix);
                symbolToQName.Add(typeIdentifier.ToUpper(CultureInfo.InvariantCulture), XmlQualifiedName.Empty);
                typeReference.Name = typeIdentifier;
                at.identifier = typeIdentifier;
            }

            return anonymousTypes;
        }

        public void RegisterMember(string identifierName)
        {
            string outputSymbol = null;
            outputSymbol = AddMember(identifierName);

            // We shouldn't be getting collisions for 
            // identifiers that are hard-coded into classes
            Debug.Assert(outputSymbol == identifierName);
        }

        public string AddMember(string identifierName)
        {
            // not making valid. Assuming this has already been done. 
            string outputSymbol = null;
            outputSymbol = getSymbol(identifierName, String.Empty);
            symbolToQName.Add(outputSymbol.ToUpper(CultureInfo.InvariantCulture), identifierName);

            return outputSymbol;
        }

        private string getSymbol(string identifierName, string suffix)
        {
            int id = 0;
            string symbol = identifierName;
            string symbolU = symbol.ToUpper(CultureInfo.InvariantCulture);
            if (symbolToQName[symbolU] == null)
            {
                return symbol;
            }

            symbol = symbol + suffix;
            symbolU = symbol.ToUpper(CultureInfo.InvariantCulture);
            string temp = symbolU;

            while (symbolToQName[symbolU] != null)
            {
                id++;
                symbolU = temp + id.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }

            if (id > 0)
                symbol = symbol + id.ToString(CultureInfo.InvariantCulture.NumberFormat);
            return symbol;
        }
    }
}
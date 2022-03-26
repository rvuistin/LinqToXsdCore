//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace Xml.Schema.Linq.CodeGen
{
    internal static class NameGenerator
    {
        static int uniqueIdCounter = 0;
        static HashSet<string> keywords;

        static NameGenerator()
        {
            keywords = new HashSet<string>(StringComparer.Ordinal)
            {
                "abstract", "event", "new", "struct", "as", "explicit", "null", "switch",
                "base", "extern", "object", "this", "bool", "false", "operator", "throw",
                "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case",
                "float", "params", "typeof", "catch", "for", "private", "uint", "char", "foreach",
                "protected", "ulong", "checked", "goto", "public", "unchecked", "class",
                "if", "readonly", "unsafe", "const", "implicit", "ref", "ushort", "continue",
                "in", "return", "using", "decimal", "int", "sbyte", "virtual", "default",
                "interface", "sealed", "volatile", "delegate", "internal", "short", "void",
                "do", "is", "sizeof", "while", "double", "lock", "stackalloc", "else", "long",
                "static", "enum", "namespace", "string", "var"
            };
        }

        public static int GetUniqueID()
        {
            Interlocked.Increment(ref uniqueIdCounter);
            return uniqueIdCounter;
        }

        public static string ChangeClrName(string clrName, NameOptions options)
        {
            switch (options)
            {
                case NameOptions.MakeCollection:
                    if (clrName[0] == '@')
                    {
                        clrName = clrName.Remove(0, 1);
                    }

                    return clrName + "Collection";

                case NameOptions.MakeList:
                    return clrName + "List";

                case NameOptions.MakePlural:
                    return clrName + "s";

                case NameOptions.MakeField:
                    return clrName + "Field";

                case NameOptions.MakeLocal:
                    return clrName + "LocalType";

                case NameOptions.MakeUnion:
                    return clrName + "UnionValue";

                case NameOptions.MakeFixedValueField:
                    return clrName + "FixedValue";

                case NameOptions.MakeParam:
                    return clrName + "Param";

                case NameOptions.MakeDefaultValueField:
                    return clrName + "DefaultValue";

                case NameOptions.MakeXName:
                    return clrName + "XName";
            }

            return clrName;
        }

        public static string GetServicesClassName()
        {
            return Constants.LinqToXsdTypeManager;
        }

        public static string MakeValidCLRNamespace(string xsdNamespace, bool nameMangler2)
        {
            if (xsdNamespace == null || xsdNamespace == string.Empty)
            {
                return string.Empty;
            }

            xsdNamespace = xsdNamespace.Replace("http://", string.Empty);
            if (xsdNamespace == string.Empty)
            {
                return string.Empty;
            }

            if (nameMangler2)
            {
                xsdNamespace = xsdNamespace.Replace('.', '_').Replace('-', '_');
            }

            string[] pieces = xsdNamespace.Split(new char[]
                {'/', '.', ':', '-'});
            string clrNS = NameGenerator.MakeValidIdentifier(pieces[0]);
            for (int i = 1; i < pieces.Length; i++)
            {
                if (pieces[i] != string.Empty)
                    clrNS = clrNS + "." + NameGenerator.MakeValidIdentifier(pieces[i]);
            }

            return clrNS;
        }

        public static string MakeValidIdentifier(string identifierName)
        {
            identifierName = CodeIdentifier.MakeValid(identifierName);
            if (isKeyword(identifierName))
                return "@" + identifierName;
            return identifierName;
        }

        public static bool isKeyword(string identifier)
        {
            return keywords.Contains(identifier);
        }
    }
}
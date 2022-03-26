//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    internal class EnumSimpleTypeInfo : ClrSimpleTypeInfo
    {
        internal EnumSimpleTypeInfo(XmlSchemaSimpleType innerType)
            : base(innerType)
        {
        }
    }
}
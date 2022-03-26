//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    internal class AtomicSimpleTypeInfo : ClrSimpleTypeInfo
    {
        internal AtomicSimpleTypeInfo(XmlSchemaType innerType)
            : base(innerType)
        {
        }
    }
}
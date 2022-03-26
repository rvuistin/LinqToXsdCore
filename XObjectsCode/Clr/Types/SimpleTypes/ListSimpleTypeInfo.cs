//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Diagnostics;
using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    internal class ListSimpleTypeInfo : ClrSimpleTypeInfo
    {
        ClrSimpleTypeInfo itemType;

        internal ListSimpleTypeInfo(XmlSchemaType innerType) : base(innerType)
        {
        }

        public ClrSimpleTypeInfo ItemType
        {
            get
            {
                if (itemType == null)
                {
                    XmlSchemaSimpleType st = InnerType as XmlSchemaSimpleType;
                    if (st == null)
                    {
                        XmlSchemaComplexType ct = InnerType as XmlSchemaComplexType;
                        st = ct.GetBaseSimpleType();
                    }

                    Debug.Assert(st.Datatype.Variety == XmlSchemaDatatypeVariety.List);
                    itemType = CreateSimpleTypeInfo(st.GetListItemType());
                }

                return itemType;
            }
        }
    }
}
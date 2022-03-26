//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Diagnostics;
using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    internal class UnionSimpleTypeInfo : ClrSimpleTypeInfo
    {
        ClrSimpleTypeInfo[] memberTypes;

        internal UnionSimpleTypeInfo(XmlSchemaType innerType) : base(innerType)
        {
        }

        public ClrSimpleTypeInfo[] MemberTypes
        {
            get
            {
                if (memberTypes == null)
                {
                    XmlSchemaSimpleType st = InnerType as XmlSchemaSimpleType;
                    if (st == null)
                    {
                        XmlSchemaComplexType ct = InnerType as XmlSchemaComplexType;
                        st = ct.GetBaseSimpleType();
                    }

                    Debug.Assert(st.Datatype.Variety == XmlSchemaDatatypeVariety.Union);
                    XmlSchemaSimpleType[] innerMemberTypes = st.GetUnionMemberTypes();

                    memberTypes = new ClrSimpleTypeInfo[innerMemberTypes.Length];
                    for (int i = 0; i < innerMemberTypes.Length; i++)
                    {
                        memberTypes[i] = CreateSimpleTypeInfo(innerMemberTypes[i]);
                    }
                }

                return memberTypes;
            }
        }
    }
}
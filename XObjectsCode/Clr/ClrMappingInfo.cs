//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    public class ClrMappingInfo
    {
        List<ClrTypeInfo> types;
        Dictionary<XmlSchemaObject, string> nameMappings;

        internal List<ClrTypeInfo> Types
        {
            get
            {
                if (types == null)
                {
                    types = new List<ClrTypeInfo>();
                }

                return types;
            }
        }

        internal Dictionary<XmlSchemaObject, string> NameMappings
        {
            get
            {
                if (nameMappings == null)
                {
                    nameMappings = new Dictionary<XmlSchemaObject, string>();
                }

                return nameMappings;
            }
            set { nameMappings = value; }
        }
    }
}

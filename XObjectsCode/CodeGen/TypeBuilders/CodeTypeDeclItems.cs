//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;

namespace Xml.Schema.Linq.CodeGen
{
    internal class CodeTypeDeclItems
    {
        public CodeConstructor functionalConstructor;
        public CodeTypeConstructor staticConstructor;
        public CodeObjectCreateExpression contentModelExpression;
        public Dictionary<string, CodeMemberProperty> propertyNameTypeTable;
        public bool hasElementWildCards;

        public CodeTypeDeclItems()
        {
        }

        public void Init()
        {
            functionalConstructor = null;
            staticConstructor = null;
            hasElementWildCards = false;
            contentModelExpression = null;
            if (propertyNameTypeTable != null)
            {
                propertyNameTypeTable.Clear();
            }
        }
    }
}

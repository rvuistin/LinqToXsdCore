//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;

using XObjects;

namespace Xml.Schema.Linq.CodeGen
{
    internal class ChoicePropertyBuilder : ContentModelPropertyBuilder
    {
        List<CodeConstructor> choiceConstructors;
        bool flatChoice; //No nested groups, no child groups and not repeating
        bool hasDuplicateType;
        Dictionary<string, ClrBasePropertyInfo> propertyTypeNameTable;

        public ChoicePropertyBuilder(ContentModelPropertyBuilder parentBuilder, GroupingInfo grouping, CodeTypeDeclaration decl, CodeTypeDeclItems declItems,
            GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public) :
            base(parentBuilder, grouping, decl, declItems, visibility)
        {
            flatChoice = !grouping.IsNested && !grouping.IsRepeating && !grouping.HasChildGroups;
            hasDuplicateType = false;
            if (flatChoice)
            {
                propertyTypeNameTable = new Dictionary<string, ClrBasePropertyInfo>();
            }
        }

        public override void GenerateConstructorCode(ClrBasePropertyInfo property)
        {
            if (flatChoice && !hasDuplicateType && property.ContentType != ContentType.WildCardProperty)
            {
                ClrBasePropertyInfo prevProperty = null;
                string propertyReturnType = property.ClrTypeName;
                if (propertyTypeNameTable.TryGetValue(propertyReturnType, out prevProperty))
                {
                    hasDuplicateType = true;
                    return;
                }
                else
                {
                    propertyTypeNameTable.Add(propertyReturnType, property);
                }

                if (choiceConstructors == null)
                {
                    choiceConstructors = new List<CodeConstructor>();
                }


                CodeConstructor choiceConstructor = CodeDomHelper.CreateConstructor(visibility.ToMemberAttribute());
                property.AddToConstructor(choiceConstructor);
                choiceConstructors.Add(choiceConstructor);
            }
        }

        public override void EndCodeGen()
        {
            if (choiceConstructors != null && !hasDuplicateType)
            {
                foreach (CodeConstructor choiceConst in choiceConstructors)
                {
                    decl.Members.Add(choiceConst);
                }
            }
        }

        public override CodeObjectCreateExpression CreateContentModelExpression()
        {
            return new CodeObjectCreateExpression(new CodeTypeReference(Constants.ChoiceContentModelEntity));
        }
    }
}
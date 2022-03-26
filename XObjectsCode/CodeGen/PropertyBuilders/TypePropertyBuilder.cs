//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Xml.Schema.Linq.CodeGen
{
    internal abstract class TypePropertyBuilder
    {
        protected CodeTypeDeclItems declItems;
        protected CodeTypeDeclaration decl;

        protected GeneratedTypesVisibility visibility;

        public TypePropertyBuilder(CodeTypeDeclaration decl, CodeTypeDeclItems declItems, GeneratedTypesVisibility visibility)
        {
            this.decl = decl;
            this.declItems = declItems;
            this.visibility = visibility;
        }

        public virtual void StartCodeGen()
        {
        }

        public virtual void GenerateCode(ClrBasePropertyInfo property, List<ClrAnnotation> annotations)
        {
            property.AddToType(decl, annotations, visibility);
        }

        public virtual void EndCodeGen()
        {
            //Do Nothing
        }

        public virtual bool IsRepeating
        {
            get { return false; }
        }

        public static TypePropertyBuilder Create(ContentModelPropertyBuilder parentBuilder, GroupingInfo groupingInfo, CodeTypeDeclaration decl,
            CodeTypeDeclItems declItems, GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public)
        {
            switch (groupingInfo.ContentModelType)
            {
                case ContentModelType.None:
                case ContentModelType.All:
                    return new DefaultPropertyBuilder(decl, declItems, visibility);

                case ContentModelType.Sequence:
                    return new SequencePropertyBuilder(parentBuilder, groupingInfo, decl, declItems, visibility);

                case ContentModelType.Choice:
                    return new ChoicePropertyBuilder(parentBuilder, groupingInfo, decl, declItems, visibility);

                default:
                    throw new InvalidOperationException();
            }
        }

        public static TypePropertyBuilder Create(CodeTypeDeclaration decl, CodeTypeDeclItems declItems,
            GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public)
        {
            return new DefaultPropertyBuilder(decl, declItems, visibility);
        }
    }
}
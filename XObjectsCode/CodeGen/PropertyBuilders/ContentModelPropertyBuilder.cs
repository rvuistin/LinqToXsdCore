//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;

namespace Xml.Schema.Linq.CodeGen
{
    internal abstract class ContentModelPropertyBuilder : TypePropertyBuilder
    {
        protected GroupingInfo grouping;
        protected CodeObjectCreateExpression contentModelExpression;

        public ContentModelPropertyBuilder(ContentModelPropertyBuilder parentBuilder, GroupingInfo grouping, CodeTypeDeclaration decl, CodeTypeDeclItems declItems,
            GeneratedTypesVisibility visibility)
            : base(decl, declItems, visibility)
        {
            this.ParentBuilder = parentBuilder;
            this.grouping = grouping; //The grouping the contentmodelbuilder works on
        }

        public ContentModelPropertyBuilder ParentBuilder { get; }

        public abstract CodeObjectCreateExpression CreateContentModelExpression();

        public virtual void GenerateConstructorCode(ClrBasePropertyInfo property)
        {
            //Do nothing for sequences and all
        }

        public override void StartCodeGen()
        {
            AddToContentModel();
        }

        public override void GenerateCode(ClrBasePropertyInfo property, List<ClrAnnotation> annotations)
        {
            GenerateConstructorCode(property);
            property.AddToType(decl, annotations, visibility);
            if (!declItems.hasElementWildCards) property.AddToContentModel(contentModelExpression);
        }

        public override string ToString()
        {
            return $"{nameof(ContentModelPropertyBuilder)} ({this.grouping})";
        }

        private void AddToContentModel()
        {
            contentModelExpression = CreateContentModelExpression();
            if (this.ParentBuilder == null)
            {
                declItems.contentModelExpression = contentModelExpression;
            }
            else
            {
                var parentContentModelExp = this.ParentBuilder.contentModelExpression;
                parentContentModelExp.Parameters.Add(contentModelExpression);
            }
        }
    }
}
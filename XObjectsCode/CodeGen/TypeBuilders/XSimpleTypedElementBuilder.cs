//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;

using XObjects;

namespace Xml.Schema.Linq.CodeGen
{
    internal class XSimpleTypedElementBuilder : TypeBuilder
    {
        string simpleTypeName;
        bool isSchemaList;

        public XSimpleTypedElementBuilder(LinqToXsdSettings settings) : base(settings) { }

        internal void Init(string simpleTypeName, bool isSchemaList)
        {
            base.InnerInit();
            this.simpleTypeName = simpleTypeName;
            this.isSchemaList = isSchemaList;
        }

        internal override CodeConstructor CreateFunctionalConstructor(List<ClrAnnotation> annotations)
        {
            //Create Constructor that takes type to wrap
            string parameterName = Constants.InnerTypeParamName;
            CodeConstructor constructor = CodeDomHelper.CreateConstructor(DefaultVisibility.ToMemberAttribute());
            CodeTypeReference returnType = null;
            if (isSchemaList)
            {
                returnType = new CodeTypeReference("IList", new CodeTypeReference(simpleTypeName));
            }
            else
            {
                returnType = new CodeTypeReference(simpleTypeName);
            }

            constructor.Parameters.Add(new CodeParameterDeclarationExpression(returnType, parameterName));

            constructor.Statements.Add(
                new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                        CodeDomHelper.This(),
                        Constants.SInnerTypePropertyName),
                    new CodeVariableReferenceExpression(parameterName)));

            ApplyAnnotations(constructor, annotations, null);
            decl.Members.Add(constructor);
            return constructor;
        }

        internal override void CreateProperty(ClrBasePropertyInfo propertyInfo, List<ClrAnnotation> annotations)
        {
            propertyInfo.AddToType(this.decl, annotations, DefaultVisibility);
        }
    }
}

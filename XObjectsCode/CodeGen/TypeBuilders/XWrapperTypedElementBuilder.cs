﻿//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

using XObjects;

namespace Xml.Schema.Linq.CodeGen
{
    internal class XWrapperTypedElementBuilder : TypeBuilder
    {
        string innerTypeName;
        string innerTypeNs;
        string memberName;
        TypeAttributes innerTypeAttributes;

        public XWrapperTypedElementBuilder(LinqToXsdSettings settings) : base(settings) { }

        internal void Init(string innerTypeFullName, string innerTypeNs, TypeAttributes innerTypeAttributes)
        {
            base.InnerInit();
            this.memberName = NameGenerator.ChangeClrName(Constants.CInnerTypePropertyName, NameOptions.MakeField);
            this.innerTypeName = innerTypeFullName;
            this.innerTypeNs = innerTypeNs;
            this.innerTypeAttributes = innerTypeAttributes;
        }

        protected override string InnerType
        {
            get { return innerTypeName; }
        }

        internal override void CreateDefaultConstructor(List<ClrAnnotation> annotations)
        {
            //create type field to wrap
            CodeMemberField typeField =
                CodeDomHelper.CreateMemberField(memberName, innerTypeName, false, MemberAttributes.Private);
            CodeFieldReferenceExpression fieldRef = CodeDomHelper.CreateFieldReference("this", memberName);

            //Create empty constructor
            CodeConstructor emptyConstructor = CodeDomHelper.CreateConstructor(DefaultVisibility.ToMemberAttribute());
            if ((innerTypeAttributes & TypeAttributes.Abstract) == 0)
            {
                //New up inner type in default constructor only if inner type is not abstract
                emptyConstructor.Statements.Add(
                    CodeDomHelper.CreateMethodCall(null, Constants.SetInnerType,
                        new CodeObjectCreateExpression(typeField.Type)));
            }
            else
            {
                //Cannot construct wrappers of abstract types using the default constructor
                emptyConstructor.Statements.Add(
                    new CodeThrowExceptionStatement(
                        new CodeObjectCreateExpression("InvalidOperationException")));
            }

            CodeConstructor dummyConstructor = null;
            if (clrTypeInfo.IsSubstitutionHead)
            {
                //Add dummy constructor that derived classes can call
                dummyConstructor = CodeDomHelper.CreateConstructor(MemberAttributes.Family);
                dummyConstructor.Parameters.Add(new CodeParameterDeclarationExpression("System.Boolean", "setNull"));
                decl.Members.Add(dummyConstructor);
            }

            if (clrTypeInfo.IsSubstitutionMember())
            {
                //Always call the dummy constructor of head from a member
                emptyConstructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(true));
                if (dummyConstructor != null)
                {
                    dummyConstructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(true));
                }
            }

            ApplyAnnotations(emptyConstructor, annotations, null);

            decl.Members.Add(typeField);
            decl.Members.Add(emptyConstructor);
            decl.Members.Add(CreateUntypedProperty(fieldRef));
            decl.Members.Add(InnerTypeProperty());
            decl.Members.Add(SetInnerType());
            if (clrTypeInfo.IsSubstitutionHead)
            {
                //Add method to set base type field in the head type from the derived members
                decl.Members.Add(SetSubstitutionMember());
            }
        }

        internal override CodeConstructor CreateFunctionalConstructor(List<ClrAnnotation> annotations)
        {
            //Create Constructor that takes type to wrap
            CodeConstructor constructor = CodeDomHelper.CreateConstructor(DefaultVisibility.ToMemberAttribute());
            if (clrTypeInfo.IsSubstitutionMember())
            {
                //If member of subst group, call dummy base constructor
                constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(true));
            }

            constructor.Parameters.Add(
                new CodeParameterDeclarationExpression(
                    new CodeTypeReference(innerTypeName), Constants.InnerTypeParamName));

            constructor.Statements.Add(CodeDomHelper.CreateMethodCall(null, Constants.SetInnerType,
                new CodeVariableReferenceExpression(Constants.InnerTypeParamName))); //SetInnerType();
            ApplyAnnotations(constructor, annotations, null);
            decl.Members.Add(constructor);
            return constructor;
        }


        internal override void CreateProperty(ClrBasePropertyInfo propertyInfo, List<ClrAnnotation> annotations)
        {
            ((ClrWrappingPropertyInfo) propertyInfo).WrappedFieldName = this.memberName;
            propertyInfo.AddToType(decl, annotations, DefaultVisibility);
        }

        protected override void ImplementCommonIXMetaData()
        {
            CodeMemberProperty localElementDictionary = CodeDomHelper.CreateInterfaceImplProperty(
                Constants.LocalElementsDictionary, Constants.IXMetaData,
                CodeDomHelper.CreateDictionaryType(Constants.XNameType, Constants.SystemTypeName));
            localElementDictionary.GetStatements.Add(CodeDomHelper.CreateCastToInterface(Constants.IXMetaData,
                "schemaMetaData", Constants.CInnerTypePropertyName));
            localElementDictionary.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodePropertyReferenceExpression(
                        new CodeVariableReferenceExpression("schemaMetaData"),
                        Constants.LocalElementsDictionary)));

            CodeMemberProperty contentProperty = CodeDomHelper.CreateInterfaceImplProperty(
                Constants.CInnerTypePropertyName, Constants.IXMetaData, new CodeTypeReference(Constants.XTypedElement));
            contentProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(),
                        Constants.CInnerTypePropertyName)));

            decl.Members.Add(localElementDictionary);
            decl.Members.Add(contentProperty);
        }

        protected override void ImplementContentModelMetaData()
        {
            decl.Members.Add(DefaultContentModel()); //No direct element children return Default content model
        }

        internal void AddTypeToTypeManager(CodeStatementCollection elementDictionaryStatements,
            CodeStatementCollection wrapperDictionaryStatements)
        {
            base.AddTypeToTypeManager(elementDictionaryStatements, Constants.ElementDictionaryField);
            var innerTypeFullName = innerTypeName.Contains(innerTypeNs)
                ? "global::" + innerTypeName
                : "global::" + innerTypeNs + "." + innerTypeName;

            wrapperDictionaryStatements.Add(CodeDomHelper.CreateMethodCallFromField(Constants.WrapperDictionaryField,
                "Add", CodeDomHelper.Typeof(clrTypeInfo.clrFullTypeName), CodeDomHelper.Typeof(innerTypeFullName)));
        }

        private CodeMethodInvokeExpression SetNameMethodCall()
        {
            return new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(Constants.XTypedServices),
                "SetName",
                CodeDomHelper.This(),
                CodeDomHelper.CreateFieldReference("this", memberName));
        }

        private CodeMemberProperty CreateUntypedProperty(CodeFieldReferenceExpression fieldRef)
        {
            //Create new XElement property so that the setter can set the wrapped object XElement as well
            CodeMemberProperty xElementProperty =
                CodeDomHelper.CreateProperty(new CodeTypeReference(Constants.XElement), true, MemberAttributes.Public); // because this is an override, it should not obey DefaultVisibility
            xElementProperty.Name = Constants.Untyped;
            xElementProperty.Attributes |= MemberAttributes.Override;

            CodePropertyReferenceExpression baseUntyped =
                new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), Constants.Untyped);
            xElementProperty.GetStatements.Add(
                new CodeMethodReturnStatement(baseUntyped));

            xElementProperty.SetStatements.Add(
                new CodeAssignStatement(
                    baseUntyped,
                    CodeDomHelper.SetValue()));

            if (clrTypeInfo.IsSubstitutionHead)
            {
                xElementProperty.SetStatements.Add(
                    new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(
                            fieldRef,
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(null)),
                        new CodeAssignStatement(
                            new CodePropertyReferenceExpression(fieldRef, Constants.Untyped),
                            CodeDomHelper.SetValue())));
            }
            else
            {
                //Field will always be non-null
                xElementProperty.SetStatements.Add(
                    new CodeAssignStatement(
                        new CodePropertyReferenceExpression(fieldRef, Constants.Untyped),
                        CodeDomHelper.SetValue()));
            }

            return xElementProperty;
        }

        private CodeMemberProperty InnerTypeProperty()
        {
            //Create InnerType Property of type T  to go with the inner type field
            CodeMemberProperty innerTypeProperty = CodeDomHelper.CreateProperty(Constants.CInnerTypePropertyName,
                new CodeTypeReference(innerTypeName), DefaultVisibility.ToMemberAttribute());
            innerTypeProperty.HasSet = false;
            if (clrTypeInfo.IsSubstitutionMember())
            {
                innerTypeProperty.Attributes |= MemberAttributes.New;
            }

            innerTypeProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    CodeDomHelper.CreateFieldReference(null, memberName)));
            return innerTypeProperty;
        }

        private CodeMemberMethod SetSubstitutionMember()
        {
            //This is for setting base type fields from types representing substitutionGroup members
            CodeMemberMethod setSubstMember =
                CodeDomHelper.CreateMethod(Constants.SetSubstitutionMember, null, MemberAttributes.Family);
            setSubstMember.Parameters.Add(
                new CodeParameterDeclarationExpression(
                    new CodeTypeReference(innerTypeName), memberName));
            setSubstMember.Statements.Add(
                new CodeAssignStatement(
                    CodeDomHelper.CreateFieldReference("this", memberName),
                    new CodeVariableReferenceExpression(memberName)));

            if (clrTypeInfo.IsSubstitutionMember())
            {
                //Add base.SetSubstitutionMember() method if this class itself is a member of another subst group
                setSubstMember.Statements.Add(CodeDomHelper.CreateMethodCall(new CodeBaseReferenceExpression(),
                    Constants.SetSubstitutionMember, new CodeVariableReferenceExpression(memberName)));
            }

            return setSubstMember;
        }

        private CodeMemberMethod SetInnerType()
        {
            CodeMemberMethod setInnerType =
                CodeDomHelper.CreateMethod(Constants.SetInnerType, null, MemberAttributes.Private);
            setInnerType.Parameters.Add(
                new CodeParameterDeclarationExpression(
                    new CodeTypeReference(innerTypeName), memberName));
            setInnerType.Statements.Add(
                new CodeAssignStatement(
                    CodeDomHelper.CreateFieldReference("this", memberName),
                    new CodeCastExpression(
                        innerTypeName,
                        new CodeMethodInvokeExpression(
                            new CodeTypeReferenceExpression(Constants.XTypedServices),
                            "GetCloneIfRooted",
                            new CodeVariableReferenceExpression(memberName)))));

            setInnerType.Statements.Add(SetNameMethodCall()); //SetName(); 
            if (clrTypeInfo.IsSubstitutionMember())
            {
                setInnerType.Statements.Add(CodeDomHelper.CreateMethodCall(new CodeBaseReferenceExpression(),
                    Constants.SetSubstitutionMember, new CodeVariableReferenceExpression(memberName)));
            }

            return setInnerType;
        }
    }
}

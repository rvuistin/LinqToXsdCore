//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

using Xml.Schema.Linq.Extensions;

using XObjects;

namespace Xml.Schema.Linq.CodeGen
{
    internal abstract class TypeBuilder
    {
        protected CodeTypeDeclaration decl;
        protected ClrTypeInfo clrTypeInfo;

        protected StateNameSource fsmNameSource;
        // this type is reused. Be sure to clear any state in Init();

        static CodeMemberMethod defaultContentModel;
        
        protected LinqToXsdSettings Settings { get; set; }

        protected GeneratedTypesVisibility DefaultVisibility
        {
            get
            {
                var typeNamespace = clrTypeInfo?.clrtypeNs ?? throw new InvalidOperationException();
                return Settings.NamespaceTypesVisibilityMap.ValueForKey(typeNamespace);
            }
        }

        protected TypeBuilder(LinqToXsdSettings settings)
        {
            Settings = settings;
        }

        internal CodeTypeDeclaration TypeDeclaration
        {
            get { return decl; }
        }

        internal virtual void CreateDefaultConstructor(List<ClrAnnotation> annotations)
        {
            decl.Members.Add(ApplyAnnotations(CodeDomHelper.CreateConstructor(DefaultVisibility.ToMemberAttribute()), annotations,
                null));
        }

        internal virtual CodeConstructor CreateFunctionalConstructor(List<ClrAnnotation> annotations)
        {
            throw new InvalidOperationException();
        }


        internal virtual void CreateFunctionalConstructor(ClrBasePropertyInfo propertyInfo,
            List<ClrAnnotation> annotations)
        {
            throw new InvalidOperationException();
        }

        internal virtual void CreateStaticConstructor()
        {
            throw new InvalidOperationException();
        }

        internal virtual void CreateAttributeProperty(ClrBasePropertyInfo propertyInfo, List<ClrAnnotation> annotations)
        {
            throw new InvalidOperationException();
        }

        internal virtual void StartGrouping(GroupingInfo grouping)
        {
            throw new InvalidOperationException();
        }

        internal virtual void EndGrouping()
        {
            throw new InvalidOperationException();
        }

        internal virtual void CreateProperty(ClrBasePropertyInfo propertyInfo, List<ClrAnnotation> annotations)
        {
            throw new InvalidOperationException();
        }

        protected virtual void SetElementWildCardFlag(bool hasAny)
        {
            //Do nothing by default
        }

        internal void AddTypeToTypeManager(CodeStatementCollection dictionaryStatements, string dictionaryName)
        {
            string typeRef = "global::" + clrTypeInfo.clrFullTypeName;
            dictionaryStatements.Add(CodeDomHelper.CreateMethodCallFromField(dictionaryName, "Add",
                CodeDomHelper.XNameGetExpression(clrTypeInfo.schemaName, clrTypeInfo.schemaNs),
                CodeDomHelper.Typeof(typeRef)));
        }

        internal virtual void ImplementInterfaces(bool enableServiceReference)
        {
            ImplementIXMetaData();
            if (enableServiceReference)
            {
                ImplementIXmlSerializable();
            }
        }

        protected void InnerInit()
        {
            decl = null;
            clrTypeInfo = null;
        }

        internal virtual void Init()
        {
            InnerInit();
        }

        protected virtual void AddBaseType()
        {
            //Set basetype
            string baseTypeClrName = clrTypeInfo.baseTypeClrName;

            if (baseTypeClrName != null)
            {
                string baseTypeClrNs = clrTypeInfo.baseTypeClrNs;
                string baseTypeRef;
                if (baseTypeClrNs.IsNotEmpty())
                    baseTypeRef = "global::" + baseTypeClrNs + "." + baseTypeClrName;
                else
                    baseTypeRef = "global::" + baseTypeClrName;
                decl.BaseTypes.Add(baseTypeRef);
            }
            else
            {
                decl.BaseTypes.Add(Constants.XTypedElement);
            }
        }

        protected virtual void ImplementContentModelMetaData()
        {
            decl.Members.Add(DefaultContentModel());
        }

        protected virtual string InnerType
        {
            get { return null; }
        }

        internal void CreateTypeDeclaration(ClrTypeInfo clrTypeInfo)
        {
            this.clrTypeInfo = clrTypeInfo;
            SetElementWildCardFlag(clrTypeInfo.HasElementWildCard);

            string schemaName = clrTypeInfo.schemaName;
            string schemaNs = clrTypeInfo.schemaNs;
            string clrTypeName = clrTypeInfo.clrtypeName;
            SchemaOrigin typeOrigin = clrTypeInfo.typeOrigin;

            CodeTypeDeclaration typeDecl = CodeDomHelper.CreateTypeDeclaration(clrTypeName, InnerType, DefaultVisibility);

            if (clrTypeInfo.IsAbstract)
            {
                typeDecl.TypeAttributes |= TypeAttributes.Abstract;
            }
            else if (clrTypeInfo.IsSealed)
            {
                typeDecl.TypeAttributes |= TypeAttributes.Sealed;
            }

            decl = typeDecl;

            AddBaseType();
            CreateServicesMembers();
            CreateDefaultConstructor(clrTypeInfo.Annotations);
        }

        internal void CreateServicesMembers()
        {
            string innerType = InnerType;
            string clrTypeName = clrTypeInfo.clrtypeName;

            bool useAutoTyping = clrTypeInfo.IsAbstract || clrTypeInfo.IsSubstitutionHead;
            if (clrTypeInfo.typeOrigin == SchemaOrigin.Element)
            {
                //Disable load and parse for complex types
                CodeTypeMember load = CodeDomHelper.CreateStaticMethod(
                    "Load", clrTypeName, innerType, "xmlFile", "System.String", useAutoTyping, DefaultVisibility);
                // http://linqtoxsd.codeplex.com/WorkItem/View.aspx?WorkItemId=4093
                var loadReader = CodeDomHelper.CreateStaticMethod(
                    "Load", clrTypeName, innerType, "xmlFile", "System.IO.TextReader", useAutoTyping, DefaultVisibility);
                CodeTypeMember parse = CodeDomHelper.CreateStaticMethod("Parse", clrTypeName, innerType, "xml",
                    "System.String", useAutoTyping, DefaultVisibility);
                if (clrTypeInfo.IsDerived)
                {
                    load.Attributes |= MemberAttributes.New;
                    parse.Attributes |= MemberAttributes.New;
                }
                else
                {
                    decl.Members.Add(CodeDomHelper.CreateSave("xmlFile", "System.String", DefaultVisibility));
                    decl.Members.Add(CodeDomHelper.CreateSave("tw", "System.IO.TextWriter", DefaultVisibility));
                    decl.Members.Add(CodeDomHelper.CreateSave("xmlWriter", "System.Xml.XmlWriter", DefaultVisibility));
                }

                decl.Members.Add(load);
                decl.Members.Add(loadReader);
                decl.Members.Add(parse);
            }

            CodeTypeMember cast = CodeDomHelper.CreateCast(clrTypeName, innerType, useAutoTyping); // dont pass default visibility; as operators must be public and static
            decl.Members.Add(cast);

            if (!clrTypeInfo.IsAbstract)
            {
                //Add Clone for non-abstract types
                CodeMemberMethod clone = CodeDomHelper.CreateMethod("Clone",
                    new CodeTypeReference(Constants.XTypedElement), MemberAttributes.Public | MemberAttributes.Override);
                if (innerType == null)
                {
                    CodeMethodInvokeExpression callClone = CodeDomHelper.CreateMethodCall(
                        new CodeTypeReferenceExpression(Constants.XTypedServices),
                        "CloneXTypedElement<" + clrTypeName + ">", new CodeThisReferenceExpression());
                    clone.Statements.Add(new CodeMethodReturnStatement(callClone));
                }
                else
                {
                    CodeMethodInvokeExpression callClone = CodeDomHelper.CreateMethodCall(
                        new CodePropertyReferenceExpression(CodeDomHelper.This(), Constants.CInnerTypePropertyName),
                        "Clone");
                    clone.Statements.Add(
                        new CodeMethodReturnStatement(
                            new CodeObjectCreateExpression(
                                clrTypeName,
                                new CodeCastExpression(
                                    new CodeTypeReference(innerType),
                                    callClone))));
                }

                decl.Members.Add(clone);
            }
        }

        protected virtual void ImplementCommonIXMetaData()
        {
            //Do nothing, this will inherit the LocalElementDictionary from XTypedElement which returns empty dict and Content which returns null
        }

        public CodeMemberProperty CreateSchemaNameProperty(string schemaName, string schemaNs, MemberAttributes attributes)
        {
            // HACK: CodeDom doesn't model readonly fields... but it doesn't check the type either!
            var field = new CodeMemberField("readonly System.Xml.Linq.XName", "xName")
            {
                Attributes = MemberAttributes.Private | MemberAttributes.Static,
                InitExpression = CodeDomHelper.XNameGetExpression(schemaName, schemaNs),
            };
            decl.Members.Add(field);

            CodeMemberProperty property = CodeDomHelper.CreateInterfaceImplProperty(Constants.SchemaName, Constants.IXMetaData,
                new CodeTypeReference(Constants.XNameType), attributes);
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, "xName")));
            return property;
        }

        private void ImplementIXMetaData()
        {
            string interfaceName = Constants.IXMetaData;

            CodeMemberProperty schemaNameProperty =
                CreateSchemaNameProperty(clrTypeInfo.schemaName, clrTypeInfo.schemaNs, DefaultVisibility.ToMemberAttribute());

            ImplementCommonIXMetaData();
            if (clrTypeInfo.HasElementWildCard) ImplementFSMMetaData();
            else ImplementContentModelMetaData();


            CodeMemberProperty typeOriginProperty = 
                CodeDomHelper.CreateTypeOriginProperty(clrTypeInfo.typeOrigin, DefaultVisibility.ToMemberAttribute());

            CodeDomHelper.AddBrowseNever(schemaNameProperty);
            CodeDomHelper.AddBrowseNever(typeOriginProperty);

            decl.Members.Add(schemaNameProperty);
            decl.Members.Add(typeOriginProperty);
            var typeManagerProperty = CodeDomHelper.CreateTypeManagerProperty(DefaultVisibility.ToMemberAttribute());
            typeManagerProperty.Attributes = MemberAttributes.FamilyOrAssembly;
            decl.Members.Add(CodeDomHelper.AddBrowseNever(typeManagerProperty));
            decl.BaseTypes.Add(interfaceName);
        }

        private void ImplementIXmlSerializable()
        {
            string interfaceName = Constants.IXmlSerializable;
            string typeManagerName = NameGenerator.GetServicesClassName();
            string methodName = clrTypeInfo.clrtypeName + "SchemaProvider";
            CodeMemberMethod schemaProviderMethod =
                CodeDomHelper.CreateMethod(methodName, null, DefaultVisibility.ToMemberAttribute() | MemberAttributes.Static);

            schemaProviderMethod.Parameters.Add(new CodeParameterDeclarationExpression("XmlSchemaSet", "schemas"));
            schemaProviderMethod.Statements.Add(
                //LinqtoXsdTypeManager.AddSchemas(schemas)
                CodeDomHelper.CreateMethodCall(new CodeTypeReferenceExpression(typeManagerName),
                    "AddSchemas", new CodeVariableReferenceExpression("schemas")));

            CodeExpression qNameExp = new CodeObjectCreateExpression("XmlQualifiedName",
                new CodePrimitiveExpression(clrTypeInfo.schemaName), new CodePrimitiveExpression(clrTypeInfo.schemaNs));

            if (clrTypeInfo.typeOrigin == SchemaOrigin.Element)
            {
                schemaProviderMethod.Statements.Add(
                    //XmlSchemaElement element = (XmlSchemaElement)schemas.GlobalElements[new XmlQualifiedName("orders", "http://tempuri/Orders.org")];
                    new CodeVariableDeclarationStatement("XmlSchemaElement", "element",
                        new CodeCastExpression("XmlSchemaElement",
                            new CodeIndexerExpression(
                                new CodePropertyReferenceExpression(
                                    new CodeVariableReferenceExpression("schemas"),
                                    "GlobalElements"), qNameExp))));

                //if(element != null) { return element.ElementSchemaType; } else { return null;}
                schemaProviderMethod.Statements.Add(
                    new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression("element"),
                            CodeBinaryOperatorType.IdentityInequality,
                            new CodePrimitiveExpression(null)),
                        new CodeMethodReturnStatement(
                            new CodePropertyReferenceExpression(
                                new CodeVariableReferenceExpression("element"),
                                "ElementSchemaType"))));

                schemaProviderMethod.Statements.Add(
                    new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));

                schemaProviderMethod.ReturnType = new CodeTypeReference("XmlSchemaType");
            }
            else
            {
                schemaProviderMethod.ReturnType = new CodeTypeReference("XmlQualifiedName");
                schemaProviderMethod.Statements.Add(
                    new CodeMethodReturnStatement(qNameExp));
            }

            decl.CustomAttributes.Add(CodeDomHelper.SchemaProviderAttribute(clrTypeInfo.clrtypeName));
            decl.BaseTypes.Add(interfaceName);
            decl.Members.Add(schemaProviderMethod);
        }

        protected virtual void ImplementFSMMetaData()
        {
            //Do nothing.
        }

        protected static CodeMemberMethod DefaultContentModel(GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public)
        {
            if (defaultContentModel == null)
            {
                CodeTypeReference cmType = new CodeTypeReference(Constants.ContentModelType);
                CodeMemberMethod getContentModelMethod =
                    CodeDomHelper.CreateInterfaceImplMethod(Constants.GetContentModel, Constants.IXMetaData, cmType, visibility);
                getContentModelMethod.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(Constants.ContentModelType),
                            Constants.Default)));
                Interlocked.CompareExchange<CodeMemberMethod>(ref defaultContentModel, getContentModelMethod, null);
            }

            return defaultContentModel;
        }

        internal static CodeTypeDeclaration CreateSimpleType(ClrSimpleTypeInfo typeInfo,
            Dictionary<XmlSchemaObject, string> nameMappings,
            LinqToXsdSettings settings)
        {
            string typeName = typeInfo is EnumSimpleTypeInfo ? typeInfo.clrtypeName + Constants.EnumValidator : typeInfo.clrtypeName;
            var simpleTypeDecl = new CodeTypeDeclaration(typeName);
            var typeVisibility = settings.NamespaceTypesVisibilityMap.ValueForKey(typeInfo.clrtypeNs).ToTypeAttribute();
            simpleTypeDecl.TypeAttributes = TypeAttributes.Sealed | typeVisibility;
            //simpleTypeDecl.TypeAttributes = TypeAttributes.Sealed | TypeAttributes.NestedAssembly;

            //Add private constructor so it cannot be instantiated
            var privateConst = new CodeConstructor { Attributes = MemberAttributes.Private };
            simpleTypeDecl.Members.Add(privateConst);

            //Create a static field for the XTypedSchemaSimpleType
            var memberVisibility = settings.NamespaceTypesVisibilityMap.ValueForKey(typeInfo.clrtypeNs).ToMemberAttribute();
            CodeMemberField typeField =
                CodeDomHelper.CreateMemberField(Constants.SimpleTypeDefInnerType, Constants.SimpleTypeValidator, false, memberVisibility | MemberAttributes.Static);
            typeField.InitExpression =
                SimpleTypeCodeDomHelper.CreateSimpleTypeDef(typeInfo, nameMappings, settings, false);

            simpleTypeDecl.Members.Add(typeField);

            // inconsistency w/ the wasy ApplyAnnotations are us
            ApplyAnnotations(simpleTypeDecl, typeInfo);

            return simpleTypeDecl;
        }

        internal static CodeTypeDeclaration CreateEnumType(EnumSimpleTypeInfo typeInfo,
            LinqToXsdSettings settings, ClrTypeInfo clrTypeInfo = null)
        {
            string typeName = typeInfo.clrtypeName;

            var enumTypeDecl = new CodeTypeDeclaration(typeName) { IsEnum = true };
            var typeVisibility = settings.NamespaceTypesVisibilityMap.ValueForKey(typeInfo.clrtypeNs).ToTypeAttribute();
            enumTypeDecl.TypeAttributes = TypeAttributes.Sealed | typeVisibility;
            foreach (var facet in typeInfo.InnerType.GetEnumFacets())
            {
                enumTypeDecl.Members.Add(new CodeMemberField(typeName, facet));
            }

            if (clrTypeInfo != null)
                enumTypeDecl.UserData[nameof(ClrTypeInfo)] = clrTypeInfo;

            ApplyAnnotations(enumTypeDecl, typeInfo);

            return enumTypeDecl;
        }

        internal void ApplyAnnotations(ClrTypeInfo typeInfo)
        {
            ApplyAnnotations(decl, typeInfo);
        }

        internal static void ApplyAnnotations(CodeMemberProperty propDecl, ClrBasePropertyInfo propInfo,
            List<ClrAnnotation> typeAnnotations)
        {
            ApplyAnnotations(propDecl, propInfo.Annotations, typeAnnotations);
        }

        internal static void ApplyAnnotations(CodeTypeMember typeDecl, ClrTypeInfo typeInfo)
        {
            ApplyAnnotations(typeDecl, typeInfo.Annotations, null);
        }

        internal static CodeTypeMember ApplyAnnotations(CodeTypeMember typeDecl, List<ClrAnnotation> annotations,
            List<ClrAnnotation> typeAnnotations)
        {
            bool fSummaryOpened = false;

            if (annotations != null)
            {
                // Do summary tags
                foreach (ClrAnnotation ann in annotations)
                {
                    if (!fSummaryOpened)
                    {
                        typeDecl.Comments.Add(new CodeCommentStatement("<summary>", true));
                        fSummaryOpened = true;
                    }

                    typeDecl.Comments.Add(new CodeCommentStatement("<para>", true));
                    typeDecl.Comments.Add(new CodeCommentStatement(ann.Text, true));
                    typeDecl.Comments.Add(new CodeCommentStatement("</para>", true));
                }
            }

            // Append any inherited annotations
            if (typeAnnotations != null)
            {
                // Do summary tags
                foreach (ClrAnnotation ann in typeAnnotations)
                {
                    // if no filter has been specified, then put everything in the statements
                    // otherwise only put the section requested
                    if (ann.Section == "summaryRegEx")
                    {
                        if (!fSummaryOpened)
                        {
                            typeDecl.Comments.Add(new CodeCommentStatement("<summary>", true));
                            fSummaryOpened = true;
                        }

                        typeDecl.Comments.Add(new CodeCommentStatement("<para>", true));
                        typeDecl.Comments.Add(new CodeCommentStatement(ann.Text, true));
                        typeDecl.Comments.Add(new CodeCommentStatement("</para>", true));
                    }
                }
            }

            // if summary was opened, then it needs to be closed
            if (fSummaryOpened)
            {
                typeDecl.Comments.Add(new CodeCommentStatement("</summary>", true));
            }

            return typeDecl;
        }

        internal static CodeTypeDeclaration CreateTypeManager(XmlQualifiedName rootElementName,
            bool enableServiceReference,
            CodeStatementCollection typeDictionaryStatements,
            CodeStatementCollection elementDictionaryStatements,
            CodeStatementCollection wrapperDictionaryStatements,
            GeneratedTypesVisibility visibility = GeneratedTypesVisibility.Public)
        {
            //Create the services type class and add members
            string servicesClassName = NameGenerator.GetServicesClassName();
            var memberVisibility = visibility.ToMemberAttribute();
            CodeTypeDeclaration servicesTypeDecl = new CodeTypeDeclaration(servicesClassName) {
                TypeAttributes = visibility.ToTypeAttribute()
            };

            //Create singleton
            CodeMemberField singletonField = CodeDomHelper.CreateMemberField(Constants.TypeManagerSingletonField,
                servicesClassName, true, MemberAttributes.Static | MemberAttributes.Private);
            CodeMemberProperty singletonProperty = CodeDomHelper.CreateProperty(Constants.TypeManagerInstance, null,
                singletonField, MemberAttributes.Static | memberVisibility, false);

            MemberAttributes privateStatic = MemberAttributes.Private | MemberAttributes.Static;
            //Create static constructor
            CodeTypeConstructor staticServicesConstructor = new CodeTypeConstructor();

            CodeTypeReference returnType = CodeDomHelper.CreateDictionaryType(Constants.XNameType, Constants.SystemTypeName);
            CodeTypeReference wrapperReturnType = CodeDomHelper.CreateDictionaryType(Constants.SystemTypeName, Constants.SystemTypeName);

            //Add private constructor so it cannot be instantiated
            var privateConst = new CodeConstructor { Attributes = MemberAttributes.Private };
            servicesTypeDecl.Members.Add(privateConst);

            //Create a dictionary of TypeName vs System.Type and the method to create it
            CodeMemberProperty typeDictProperty = null;
            if (typeDictionaryStatements.Count > 0)
            {
                typeDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.GlobalTypeDictionary,
                    Constants.ILinqToXsdTypeManager, returnType, Constants.TypeDictionaryField, MemberAttributes.Private);

                CodeMemberField staticTypeDictionary =
                    CodeDomHelper.CreateDictionaryField(Constants.TypeDictionaryField, Constants.XNameType, Constants.SystemTypeName,
                        MemberAttributes.Private);
                CodeMemberMethod buildTypeDictionary =
                    CodeDomHelper.CreateMethod(Constants.BuildTypeDictionary, null, privateStatic);
                buildTypeDictionary.Statements.AddRange(typeDictionaryStatements);

                staticServicesConstructor.Statements.Add(
                    CodeDomHelper.CreateMethodCall(null, Constants.BuildTypeDictionary));
                servicesTypeDecl.Members.Add(staticTypeDictionary);
                servicesTypeDecl.Members.Add(buildTypeDictionary);
            }
            else
            {
                typeDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.GlobalTypeDictionary,
                    Constants.ILinqToXsdTypeManager, returnType, MemberAttributes.Private);
                typeDictProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(Constants.XTypedServices),
                            Constants.EmptyDictionaryField)));
            }

            //Create a dictionary of ElementName Vs System.Type - For Auto typing and substitutionGroups
            CodeMemberProperty elementDictProperty = null;
            if (elementDictionaryStatements.Count > 0)
            {
                elementDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.GlobalElementDictionary,
                    Constants.ILinqToXsdTypeManager, returnType, Constants.ElementDictionaryField, MemberAttributes.Private);

                CodeMemberField staticElementDictionary =
                    CodeDomHelper.CreateDictionaryField(Constants.ElementDictionaryField, Constants.XNameType, Constants.SystemTypeName,
                        MemberAttributes.Private);
                CodeMemberMethod buildElementDictionary =
                    CodeDomHelper.CreateMethod(Constants.BuildElementDictionary, null, privateStatic);
                buildElementDictionary.Statements.AddRange(elementDictionaryStatements);

                staticServicesConstructor.Statements.Add(
                    CodeDomHelper.CreateMethodCall(null, Constants.BuildElementDictionary));
                servicesTypeDecl.Members.Add(staticElementDictionary);
                servicesTypeDecl.Members.Add(buildElementDictionary);
            }
            else
            {
                elementDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.GlobalElementDictionary,
                    Constants.ILinqToXsdTypeManager, returnType, MemberAttributes.Private);
                elementDictProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(Constants.XTypedServices),
                            Constants.EmptyDictionaryField)));
            }

            //Create a dictionary of Wrapper Element Type Vs Wrapper Type - For Auto typing when casting from XElement to Type
            CodeMemberProperty wrapperDictProperty = null;
            if (wrapperDictionaryStatements.Count > 0)
            {
                wrapperDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.RootContentTypeMapping,
                    Constants.ILinqToXsdTypeManager, wrapperReturnType, Constants.WrapperDictionaryField);

                CodeMemberField staticWrapperDictionary =
                    CodeDomHelper.CreateDictionaryField(Constants.WrapperDictionaryField, Constants.SystemTypeName, Constants.SystemTypeName,
                        MemberAttributes.Private);
                CodeMemberMethod buildWrapperDictionary =
                    CodeDomHelper.CreateMethod(Constants.BuildWrapperDictionary, null, privateStatic);
                buildWrapperDictionary.Statements.AddRange(wrapperDictionaryStatements);

                staticServicesConstructor.Statements.Add(
                    CodeDomHelper.CreateMethodCall(null, Constants.BuildWrapperDictionary));
                servicesTypeDecl.Members.Add(staticWrapperDictionary);
                servicesTypeDecl.Members.Add(buildWrapperDictionary);
            }
            else
            {
                wrapperDictProperty = CodeDomHelper.CreateInterfaceImplProperty(Constants.RootContentTypeMapping,
                    Constants.ILinqToXsdTypeManager, wrapperReturnType);
                wrapperDictProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression(Constants.XTypedServices),
                            Constants.EmptyTypeMappingDictionary)));
            }

            //Implement IXmlSerializable AddSchemas method for the XmlSchemaProvider method and Schemas get set property for runtime access to schemas
            //if (enableServiceReference) { //Since property is on the interface, it has to be implemented;
            string schemaSetFieldName = "schemaSet";
            CodeTypeReference schemaSetType = new CodeTypeReference("XmlSchemaSet");

            CodeMemberField schemaSetField = new CodeMemberField(schemaSetType, schemaSetFieldName);
            schemaSetField.Attributes = MemberAttributes.Private | MemberAttributes.Static;

            //AddSchemas method
            CodeMemberMethod addSchemasMethod = CodeDomHelper.CreateMethod("AddSchemas", null, MemberAttributes.FamilyOrAssembly | MemberAttributes.Static);
            addSchemasMethod.Parameters.Add(new CodeParameterDeclarationExpression("XmlSchemaSet", "schemas"));
            //schemas.Add(schemaSet);
            addSchemasMethod.Statements.Add(CodeDomHelper.CreateMethodCall(
                new CodeVariableReferenceExpression("schemas"), "Add",
                new CodeFieldReferenceExpression(null, schemaSetFieldName)));


            CodeTypeReferenceExpression interLockedType =
                new CodeTypeReferenceExpression("System.Threading.Interlocked");

            CodeMemberProperty schemaSetProperty =
                CodeDomHelper.CreateInterfaceImplProperty("Schemas", Constants.ILinqToXsdTypeManager, schemaSetType, memberVisibility);
            CodeFieldReferenceExpression schemaSetFieldRef = new CodeFieldReferenceExpression(null, schemaSetFieldName);

            CodeDirectionExpression schemaSetParam = new CodeDirectionExpression(FieldDirection.Ref, schemaSetFieldRef);

            schemaSetProperty.GetStatements.Add(
                new CodeConditionStatement(
                    new CodeBinaryOperatorExpression(schemaSetFieldRef,
                        CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)),
                    new CodeVariableDeclarationStatement(schemaSetType, "tempSet",
                        new CodeObjectCreateExpression(schemaSetType)),
                    new CodeExpressionStatement(
                        CodeDomHelper.CreateMethodCall(interLockedType, "CompareExchange",
                            schemaSetParam,
                            new CodeVariableReferenceExpression("tempSet"),
                            new CodePrimitiveExpression(null)))));

            schemaSetProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression(schemaSetFieldName)));

            //Setter
            schemaSetProperty.SetStatements.Add(new CodeAssignStatement(schemaSetFieldRef,
                new CodePropertySetValueReferenceExpression()));

            servicesTypeDecl.Members.Add(schemaSetField);
            servicesTypeDecl.Members.Add(schemaSetProperty);
            servicesTypeDecl.Members.Add(addSchemasMethod);
            //}
            //Implement ILinqToXsdTypeManager
            servicesTypeDecl.Members.Add(typeDictProperty);
            servicesTypeDecl.Members.Add(elementDictProperty);
            servicesTypeDecl.Members.Add(wrapperDictProperty);
            servicesTypeDecl.BaseTypes.Add(Constants.ILinqToXsdTypeManager);


            //Add a getter that will get the root type name
            CodeMemberMethod getRootType = new CodeMemberMethod();
            getRootType.Attributes = MemberAttributes.Static | memberVisibility;
            getRootType.Name = Constants.GetRootType;
            getRootType.ReturnType = new CodeTypeReference(Constants.SystemTypeName);
            if (rootElementName.IsEmpty)
            {
                getRootType.Statements.Add(
                    new CodeMethodReturnStatement(
                        CodeDomHelper.Typeof("Xml.Schema.Linq.XTypedElement")));
            }
            else
            {
                getRootType.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeIndexerExpression(
                            CodeDomHelper.CreateFieldReference(null, Constants.ElementDictionaryField),
                            CodeDomHelper.XNameGetExpression(rootElementName.Name,
                                rootElementName.Namespace))));
            }

            servicesTypeDecl.Members.Add(staticServicesConstructor);
            servicesTypeDecl.Members.Add(getRootType);
            servicesTypeDecl.Members.Add(singletonField);
            servicesTypeDecl.Members.Add(singletonProperty);
            return servicesTypeDecl;
        }

        public override string ToString() => $"{nameof(TypeBuilder)} ({this.clrTypeInfo})";
    }
}

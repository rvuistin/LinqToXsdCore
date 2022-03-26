//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xml.Schema.Linq.CodeGen
{
    internal class XTypedElementBuilder : TypeBuilder
    {
        CodeTypeDeclItems declItemsInfo;
        Stack<TypePropertyBuilder> propertyBuilderStack;
        TypePropertyBuilder propertyBuilder;
        CodeStatementCollection propertyDictionaryAddStatements;


        internal XTypedElementBuilder(LinqToXsdSettings settings): base(settings)
        {
            InnerInit();
        }

        // InnerInit is a non-virtual function to
        // prevent virtual methods from being called
        // in the call stack of the constructor
        protected new void InnerInit()
        {
            base.InnerInit();
            propertyBuilder = null;
            if (propertyBuilderStack != null)
            {
                propertyBuilderStack.Clear();
            }

            if (propertyDictionaryAddStatements != null)
            {
                propertyDictionaryAddStatements.Clear();
            }

            if (declItemsInfo == null)
            {
                declItemsInfo = new CodeTypeDeclItems();
            }
            else
            {
                declItemsInfo.Init();
            }
        }

        internal override void Init()
        {
            InnerInit();
        }

        protected override void SetElementWildCardFlag(bool hasAny)
        {
            declItemsInfo.hasElementWildCards = hasAny;
        }

        internal override void StartGrouping(GroupingInfo groupingInfo)
        {
            InitializeTables();
            propertyBuilder = TypePropertyBuilder.Create(propertyBuilder as ContentModelPropertyBuilder, groupingInfo, decl, declItemsInfo, DefaultVisibility);
            propertyBuilder.StartCodeGen(); //Start the group's code gen, like setting up functional const etc
            propertyBuilderStack.Push(propertyBuilder);
        }

        internal override void CreateProperty(ClrBasePropertyInfo propertyInfo, List<ClrAnnotation> annotations)
        {
            if (clrTypeInfo.InlineBaseType && propertyInfo.FromBaseType)
            {
                propertyInfo.IsNew = true;
            }

            propertyBuilder.GenerateCode(propertyInfo, annotations);
            if ((propertyInfo.ContentType == ContentType.Property) && !propertyInfo.IsDuplicate)
            {
                //Do not add repeating properties to the LocalElementDictionary of type
                propertyDictionaryAddStatements.Add(CodeDomHelper.CreateMethodCallFromField(
                    Constants.LocalElementDictionaryField, "Add",
                    propertyInfo.GetXName(),
                    CodeDomHelper.Typeof(propertyInfo.ClrTypeName)));
            }
        }

        internal override void EndGrouping()
        {
            propertyBuilder.EndCodeGen();
            propertyBuilderStack.Pop(); //Remove current property builder
            if (propertyBuilderStack.Count > 0)
            {
                propertyBuilder =
                    propertyBuilderStack.Peek(); //Re-set property builder to parent group's property builder
            }
        }

        internal override void CreateAttributeProperty(ClrBasePropertyInfo propertyInfo,
            List<ClrAnnotation> annotations)
        {
            propertyBuilder = TypePropertyBuilder.Create(decl, declItemsInfo, DefaultVisibility);
            propertyBuilder.GenerateCode(propertyInfo, annotations);
        }

        internal override CodeConstructor CreateFunctionalConstructor(List<ClrAnnotation> annotations)
        {
            CodeConstructor functionalConstructor = declItemsInfo.functionalConstructor;
            if (functionalConstructor != null && functionalConstructor.Parameters.Count > 0)
            {
                ApplyAnnotations(functionalConstructor, annotations, null);
                decl.Members.Add(functionalConstructor);
            }

            return functionalConstructor;
        }

        internal override void CreateStaticConstructor()
        {
            if (declItemsInfo.staticConstructor == null)
            {
                declItemsInfo.staticConstructor = new CodeTypeConstructor();
                decl.Members.Add(declItemsInfo.staticConstructor);
            }
        }

        public override string ToString() => $"{nameof(XTypedElementBuilder)} ({this.clrTypeInfo})";

        protected override void ImplementCommonIXMetaData()
        {
            CodeMemberProperty localElementDictionary = null;
            if (HasElementProperties)
            {
                CreateStaticConstructor();
                localElementDictionary = BuildLocalElementDictionary();
                declItemsInfo.staticConstructor.Statements.Add(
                    CodeDomHelper.CreateMethodCall(null, "BuildElementDictionary"));
                decl.Members.Add(localElementDictionary);
            }
        }

        protected override void ImplementContentModelMetaData()
        {
            CodeMemberMethod getContentModelMethod = null;

            if (HasElementProperties)
            {
                if (declItemsInfo.contentModelExpression != null)
                {
                    //Create static constr for the content model of the type
                    CodeTypeReference cmType = new CodeTypeReference(Constants.ContentModelType);

                    declItemsInfo.staticConstructor.Statements
                                 .Add( // contentModel = new Sequence/Choice/AllContentModel(...);
                                     new CodeAssignStatement(
                                         new CodeVariableReferenceExpression(Constants.ContentModelMember),
                                         declItemsInfo.contentModelExpression));

                    //Add static field to store the constructed content model
                    CodeMemberField contentModelField = new CodeMemberField(cmType, Constants.ContentModelMember);
                    CodeDomHelper.AddBrowseNever(contentModelField);
                    contentModelField.Attributes = MemberAttributes.Private | MemberAttributes.Static;

                    decl.Members.Add(contentModelField);

                    //Create Method impl
                    getContentModelMethod = CodeDomHelper.CreateInterfaceImplMethod(Constants.GetContentModel,
                        Constants.IXMetaData, cmType, Constants.ContentModelMember);
                }
                else
                {
                    //Return Default content model
                    getContentModelMethod = DefaultContentModel();
                }
            }
            else
            {
                //No element children per schema
                if (this.clrTypeInfo.IsDerived)
                {
                    //Probably derived by restriction, use base content model
                    return;
                }
                else
                {
                    //Return Default content model
                    getContentModelMethod = DefaultContentModel();
                }
            }

            decl.Members.Add(getContentModelMethod);
        }

        protected override void ImplementFSMMetaData()
        {
            Debug.Assert(clrTypeInfo.HasElementWildCard);

            if (fsmNameSource == null) fsmNameSource = new StateNameSource();
            else fsmNameSource.Reset();

            FSM fsm = clrTypeInfo.CreateFSM(fsmNameSource);

            //Add a member field: private static FSM fsm;
            decl.Members.Add(CodeDomHelper.CreateMemberField(Constants.FSMMember, Constants.FSMClass, false, MemberAttributes.Private | MemberAttributes.Static));

            //Add a function: FSM  FSM IXMetaData.GetFSM() {return fsm}
            CodeMemberMethod getFSM =
                CodeDomHelper.CreateInterfaceImplMethod(Constants.GetFSM, Constants.IXMetaData,
                    new CodeTypeReference(Constants.FSMClass));

            getFSM.Statements.Add(
                new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, Constants.FSMMember)));
            decl.Members.Add(getFSM);

            //Add InitFSM() and construct the FSM
            CodeMemberMethod initFSM =
                CodeDomHelper.CreateMethod(Constants.InitFSM,
                    new CodeTypeReference(), MemberAttributes.Private | MemberAttributes.Static);
            FSMCodeDomHelper.CreateFSMStmt(fsm, initFSM.Statements);
            decl.Members.Add(initFSM);

            CreateStaticConstructor();
            declItemsInfo.staticConstructor.Statements.Add(
                CodeDomHelper.CreateMethodCall(null, Constants.InitFSM, null));
        }

        private bool HasElementProperties
        {
            get { return propertyDictionaryAddStatements != null && propertyDictionaryAddStatements.Count > 0; }
        }

        private CodeMemberProperty BuildLocalElementDictionary()
        {
            CodeMemberProperty localDictionaryProperty = CodeDomHelper.CreateInterfaceImplProperty(
                Constants.LocalElementsDictionary, Constants.IXMetaData,
                CodeDomHelper.CreateDictionaryType(Constants.XNameType, Constants.SystemTypeName));

            //new override for derived classes
            CodeMemberField localDictionaryField =
                CodeDomHelper.CreateDictionaryField(Constants.LocalElementDictionaryField, Constants.XNameType, Constants.SystemTypeName, MemberAttributes.Private);
            CodeMemberMethod localDictionaryMethod = CodeDomHelper.CreateMethod(Constants.BuildElementDictionary, null, MemberAttributes.Private | MemberAttributes.Static);
            localDictionaryMethod.Statements.AddRange(propertyDictionaryAddStatements);

            decl.Members.Add(localDictionaryField);
            decl.Members.Add(localDictionaryMethod);
            localDictionaryProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    CodeDomHelper.CreateFieldReference(null,
                        Constants.LocalElementDictionaryField)));


            CodeDomHelper.AddBrowseNever(localDictionaryProperty);
            CodeDomHelper.AddBrowseNever(localDictionaryField);
            return localDictionaryProperty;
        }

        private void InitializeTables()
        {
            if (propertyBuilderStack == null)
            {
                propertyBuilderStack = new Stack<TypePropertyBuilder>();
            }

            if (propertyDictionaryAddStatements == null)
            {
                //Allocate this since the properies within a grouping will need to be added to the type's element dictionary
                propertyDictionaryAddStatements = new CodeStatementCollection();
            }

            if (declItemsInfo.propertyNameTypeTable == null)
            {
                declItemsInfo.propertyNameTypeTable = new Dictionary<string, CodeMemberProperty>();
            }
        }
    }
}

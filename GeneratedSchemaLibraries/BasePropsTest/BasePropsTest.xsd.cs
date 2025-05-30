//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqToXsd.Schemas.Test.BasePropsTypes {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    
    
    /// <summary>
    /// <para>
    /// Regular expression: (BaseProp)
    /// </para>
    /// </summary>
    public partial class BaseContextType : XTypedElement, IXMetaData {
        
		public static explicit operator BaseContextType(XElement xe) { return XTypedServices.ToXTypedElement<BaseContextType>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public override XTypedElement Clone() {
            return XTypedServices.CloneXTypedElement<BaseContextType>(this);
        }
        
        /// <summary>
        /// <para>
        /// Regular expression: (BaseProp)
        /// </para>
        /// </summary>
        public BaseContextType() {
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal static readonly System.Xml.Linq.XName BasePropXName = System.Xml.Linq.XName.Get("BaseProp", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        /// <summary>
        /// <para>
        /// Occurrence: required
        /// </para>
        /// <para>
        /// Regular expression: (BaseProp)
        /// </para>
        /// </summary>
        public virtual string BaseProp {
            get {
                XElement x = this.GetElement(BasePropXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetElement(BasePropXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("BaseContextType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        static BaseContextType() {
            BuildElementDictionary();
            contentModel = new SequenceContentModelEntity(new NamedContentModelEntity(BasePropXName));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<System.Xml.Linq.XName, System.Type> localElementDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static void BuildElementDictionary() {
            localElementDictionary.Add(BasePropXName, typeof(string));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Dictionary<System.Xml.Linq.XName, System.Type> IXMetaData.LocalElementsDictionary {
            get {
                return localElementDictionary;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ContentModelEntity contentModel;
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return contentModel;
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.Xml.Linq.XName IXMetaData.SchemaName {
            get {
                return xName;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        SchemaOrigin IXMetaData.TypeOrigin {
            get {
                return SchemaOrigin.Fragment;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILinqToXsdTypeManager IXMetaData.TypeManager {
            get {
                return LinqToXsdTypeManager.Instance;
            }
        }
    }
    
    /// <summary>
    /// <para>
    /// Regular expression: (BaseContext)
    /// </para>
    /// </summary>
    public partial class BaseType : XTypedElement, IXMetaData {
        
		public static explicit operator BaseType(XElement xe) { return XTypedServices.ToXTypedElement<BaseType>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public override XTypedElement Clone() {
            return XTypedServices.CloneXTypedElement<BaseType>(this);
        }
        
        /// <summary>
        /// <para>
        /// Regular expression: (BaseContext)
        /// </para>
        /// </summary>
        public BaseType() {
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal static readonly System.Xml.Linq.XName BaseContextXName = System.Xml.Linq.XName.Get("BaseContext", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        /// <summary>
        /// <para>
        /// Occurrence: required
        /// </para>
        /// <para>
        /// Regular expression: (BaseContext)
        /// </para>
        /// </summary>
        public virtual BaseContextType BaseContext {
            get {
                XElement x = this.GetElement(BaseContextXName);
                return ((BaseContextType)(x));
            }
            set {
                this.SetElement(BaseContextXName, value);
            }
        }
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("BaseType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        static BaseType() {
            BuildElementDictionary();
            contentModel = new SequenceContentModelEntity(new NamedContentModelEntity(BaseContextXName));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<System.Xml.Linq.XName, System.Type> localElementDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static void BuildElementDictionary() {
            localElementDictionary.Add(BaseContextXName, typeof(BaseContextType));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Dictionary<System.Xml.Linq.XName, System.Type> IXMetaData.LocalElementsDictionary {
            get {
                return localElementDictionary;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ContentModelEntity contentModel;
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return contentModel;
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.Xml.Linq.XName IXMetaData.SchemaName {
            get {
                return xName;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        SchemaOrigin IXMetaData.TypeOrigin {
            get {
                return SchemaOrigin.Fragment;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILinqToXsdTypeManager IXMetaData.TypeManager {
            get {
                return LinqToXsdTypeManager.Instance;
            }
        }
    }
    
    /// <summary>
    /// <para>
    /// Regular expression: (BaseContext, ContentProp)
    /// </para>
    /// </summary>
    public partial class ContentType : global::LinqToXsd.Schemas.Test.BasePropsTypes.BaseType, IXMetaData {
        
		public static explicit operator ContentType(XElement xe) { return XTypedServices.ToXTypedElement<ContentType>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public override XTypedElement Clone() {
            return XTypedServices.CloneXTypedElement<ContentType>(this);
        }
        
        /// <summary>
        /// <para>
        /// Regular expression: (BaseContext, ContentProp)
        /// </para>
        /// </summary>
        public ContentType() {
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal static readonly System.Xml.Linq.XName ContentPropXName = System.Xml.Linq.XName.Get("ContentProp", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        /// <summary>
        /// <para>
        /// Occurrence: required
        /// </para>
        /// <para>
        /// Regular expression: (BaseContext, ContentProp)
        /// </para>
        /// </summary>
        public virtual string ContentProp {
            get {
                XElement x = this.GetElement(ContentPropXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetElement(ContentPropXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("ContentType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        static ContentType() {
            BuildElementDictionary();
            contentModel = new SequenceContentModelEntity(new NamedContentModelEntity(BaseContextXName), new NamedContentModelEntity(ContentPropXName));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Dictionary<System.Xml.Linq.XName, System.Type> localElementDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static void BuildElementDictionary() {
            localElementDictionary.Add(BaseContextXName, typeof(BaseContextType));
            localElementDictionary.Add(ContentPropXName, typeof(string));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Dictionary<System.Xml.Linq.XName, System.Type> IXMetaData.LocalElementsDictionary {
            get {
                return localElementDictionary;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ContentModelEntity contentModel;
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return contentModel;
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.Xml.Linq.XName IXMetaData.SchemaName {
            get {
                return xName;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        SchemaOrigin IXMetaData.TypeOrigin {
            get {
                return SchemaOrigin.Fragment;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILinqToXsdTypeManager IXMetaData.TypeManager {
            get {
                return LinqToXsdTypeManager.Instance;
            }
        }
    }
    
    public partial class Wrapper : XTypedElement, IXMetaData {
        
        public void Save(string xmlFile) {
            XTypedServices.Save(xmlFile, Untyped);
        }
        
        public void Save(System.IO.TextWriter tw) {
            XTypedServices.Save(tw, Untyped);
        }
        
        public void Save(System.Xml.XmlWriter xmlWriter) {
            XTypedServices.Save(xmlWriter, Untyped);
        }
        
        public static Wrapper Load(string xmlFile) {
            return XTypedServices.Load<Wrapper, ContentType>(xmlFile, LinqToXsdTypeManager.Instance);
        }
        
        public static Wrapper Load(System.IO.TextReader xmlFile) {
            return XTypedServices.Load<Wrapper, ContentType>(xmlFile, LinqToXsdTypeManager.Instance);
        }
        
        public static Wrapper Parse(string xml) {
            return XTypedServices.Parse<Wrapper, ContentType>(xml, LinqToXsdTypeManager.Instance);
        }
        
		public static explicit operator Wrapper(XElement xe) { return XTypedServices.ToXTypedElement<Wrapper, ContentType>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public override XTypedElement Clone() {
            return new Wrapper(((ContentType)(this.Content.Clone())));
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ContentType ContentField;
        
        public Wrapper() {
            SetInnerType(new ContentType());
        }
        
        public override XElement Untyped {
            get {
                return base.Untyped;
            }
            set {
                base.Untyped = value;
                this.ContentField.Untyped = value;
            }
        }
        
        public virtual ContentType Content {
            get {
                return ContentField;
            }
        }
        
        private void SetInnerType(ContentType ContentField) {
            this.ContentField = ((ContentType)(XTypedServices.GetCloneIfRooted(ContentField)));
            XTypedServices.SetName(this, this.ContentField);
        }
        
        public Wrapper(ContentType content) {
            SetInnerType(content);
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: required
        /// </para>
        /// <para>
        /// Regular expression: (BaseContext, ContentProp)
        /// </para>
        /// </summary>
        public virtual string ContentProp {
            get {
                return this.ContentField.ContentProp;
            }
            set {
                this.ContentField.ContentProp = value;
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: required
        /// </para>
        /// <para>
        /// Regular expression: (BaseContext)
        /// </para>
        /// </summary>
        public virtual BaseContextType BaseContext {
            get {
                return this.ContentField.BaseContext;
            }
            set {
                this.ContentField.BaseContext = value;
            }
        }
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("Wrapper", "http://linqtoxsd.schemas.org/test/base-props-types.xsd");
        
        Dictionary<System.Xml.Linq.XName, System.Type> IXMetaData.LocalElementsDictionary {
            get {
                IXMetaData schemaMetaData = ((IXMetaData)(this.Content));
                return schemaMetaData.LocalElementsDictionary;
            }
        }
        
        XTypedElement IXMetaData.Content {
            get {
                return this.Content;
            }
        }
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return ContentModelEntity.Default;
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        System.Xml.Linq.XName IXMetaData.SchemaName {
            get {
                return xName;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        SchemaOrigin IXMetaData.TypeOrigin {
            get {
                return SchemaOrigin.Element;
            }
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILinqToXsdTypeManager IXMetaData.TypeManager {
            get {
                return LinqToXsdTypeManager.Instance;
            }
        }
    }
    
    public class LinqToXsdTypeManager : ILinqToXsdTypeManager {
        
        private LinqToXsdTypeManager() {
        }
        
        private static Dictionary<System.Xml.Linq.XName, System.Type> typeDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static void BuildTypeDictionary() {
            typeDictionary.Add(System.Xml.Linq.XName.Get("BaseContextType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd"), typeof(global::LinqToXsd.Schemas.Test.BasePropsTypes.BaseContextType));
            typeDictionary.Add(System.Xml.Linq.XName.Get("BaseType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd"), typeof(global::LinqToXsd.Schemas.Test.BasePropsTypes.BaseType));
            typeDictionary.Add(System.Xml.Linq.XName.Get("ContentType", "http://linqtoxsd.schemas.org/test/base-props-types.xsd"), typeof(global::LinqToXsd.Schemas.Test.BasePropsTypes.ContentType));
        }
        
        private static Dictionary<System.Xml.Linq.XName, System.Type> elementDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static void BuildElementDictionary() {
            elementDictionary.Add(System.Xml.Linq.XName.Get("Wrapper", "http://linqtoxsd.schemas.org/test/base-props-types.xsd"), typeof(global::LinqToXsd.Schemas.Test.BasePropsTypes.Wrapper));
        }
        
        private static Dictionary<System.Type, System.Type> wrapperDictionary = new Dictionary<System.Type, System.Type>();
        
        private static void BuildWrapperDictionary() {
            wrapperDictionary.Add(typeof(LinqToXsd.Schemas.Test.BasePropsTypes.Wrapper), typeof(global::LinqToXsd.Schemas.Test.BasePropsTypes.ContentType));
        }
        
        private static XmlSchemaSet schemaSet;
        
        XmlSchemaSet ILinqToXsdTypeManager.Schemas {
            get {
                if ((schemaSet == null)) {
                    XmlSchemaSet tempSet = new XmlSchemaSet();
                    System.Threading.Interlocked.CompareExchange(ref schemaSet, tempSet, null);
                }
                return schemaSet;
            }
            set {
                schemaSet = value;
            }
        }
        
        protected internal static void AddSchemas(XmlSchemaSet schemas) {
            schemas.Add(schemaSet);
        }
        
        Dictionary<System.Xml.Linq.XName, System.Type> ILinqToXsdTypeManager.GlobalTypeDictionary {
            get {
                return typeDictionary;
            }
        }
        
        Dictionary<System.Xml.Linq.XName, System.Type> ILinqToXsdTypeManager.GlobalElementDictionary {
            get {
                return elementDictionary;
            }
        }
        
        Dictionary<System.Type, System.Type> ILinqToXsdTypeManager.RootContentTypeMapping {
            get {
                return wrapperDictionary;
            }
        }
        
        static LinqToXsdTypeManager() {
            BuildTypeDictionary();
            BuildElementDictionary();
            BuildWrapperDictionary();
        }
        
        public static System.Type GetRootType() {
            return elementDictionary[System.Xml.Linq.XName.Get("Wrapper", "http://linqtoxsd.schemas.org/test/base-props-types.xsd")];
        }
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static LinqToXsdTypeManager typeManagerSingleton = new LinqToXsdTypeManager();
        
        public static LinqToXsdTypeManager Instance {
            get {
                return typeManagerSingleton;
            }
        }
    }
    
    public partial class XRootNamespace {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XDocument doc;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;
        
        private XRootNamespace() {
        }
        
        public static XRootNamespace Load(string xmlFile) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Load(xmlFile);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Load(string xmlFile, LoadOptions options) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Load(xmlFile, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Load(TextReader textReader) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Load(textReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Load(TextReader textReader, LoadOptions options) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Load(textReader, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Load(XmlReader xmlReader) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Load(xmlReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Parse(string text) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Parse(text);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRootNamespace Parse(string text, LoadOptions options) {
            XRootNamespace root = new XRootNamespace();
            root.doc = XDocument.Parse(text, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public virtual void Save(string fileName) {
            doc.Save(fileName);
        }
        
        public virtual void Save(TextWriter textWriter) {
            doc.Save(textWriter);
        }
        
        public virtual void Save(XmlWriter writer) {
            doc.Save(writer);
        }
        
        public virtual void Save(TextWriter textWriter, SaveOptions options) {
            doc.Save(textWriter, options);
        }
        
        public virtual void Save(string fileName, SaveOptions options) {
            doc.Save(fileName, options);
        }
        
        public virtual XDocument XDocument {
            get {
                return doc;
            }
        }
        
        public virtual XTypedElement Root {
            get {
                return rootObject;
            }
        }
        
        public XRootNamespace(Wrapper root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
        }
        

		public Wrapper Wrapper {  get {return rootObject as Wrapper; } }
    }
    
    public partial class XRoot {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XDocument doc;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;
        
        private XRoot() {
        }
        
        public static XRoot Load(string xmlFile) {
            XRoot root = new XRoot();
            root.doc = XDocument.Load(xmlFile);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Load(string xmlFile, LoadOptions options) {
            XRoot root = new XRoot();
            root.doc = XDocument.Load(xmlFile, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Load(TextReader textReader) {
            XRoot root = new XRoot();
            root.doc = XDocument.Load(textReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Load(TextReader textReader, LoadOptions options) {
            XRoot root = new XRoot();
            root.doc = XDocument.Load(textReader, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Load(XmlReader xmlReader) {
            XRoot root = new XRoot();
            root.doc = XDocument.Load(xmlReader);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Parse(string text) {
            XRoot root = new XRoot();
            root.doc = XDocument.Parse(text);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public static XRoot Parse(string text, LoadOptions options) {
            XRoot root = new XRoot();
            root.doc = XDocument.Parse(text, options);
            XTypedElement typedRoot = XTypedServices.ToXTypedElement(root.doc.Root, LinqToXsdTypeManager.Instance);
            if ((typedRoot == null)) {
                throw new LinqToXsdException("Invalid root element in xml document.");
            }
            root.rootObject = typedRoot;
            return root;
        }
        
        public virtual void Save(string fileName) {
            doc.Save(fileName);
        }
        
        public virtual void Save(TextWriter textWriter) {
            doc.Save(textWriter);
        }
        
        public virtual void Save(XmlWriter writer) {
            doc.Save(writer);
        }
        
        public virtual void Save(TextWriter textWriter, SaveOptions options) {
            doc.Save(textWriter, options);
        }
        
        public virtual void Save(string fileName, SaveOptions options) {
            doc.Save(fileName, options);
        }
        
        public virtual XDocument XDocument {
            get {
                return doc;
            }
        }
        
        public virtual XTypedElement Root {
            get {
                return rootObject;
            }
        }
        
        public XRoot(global::LinqToXsd.Schemas.Test.BasePropsTypes.Wrapper root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
        }
        

		public global::LinqToXsd.Schemas.Test.BasePropsTypes.Wrapper Wrapper {  get {return rootObject as global::LinqToXsd.Schemas.Test.BasePropsTypes.Wrapper; } }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace www.niso.org.schemas.ali {
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
    using www.w3.org.XML.Item1998.@namespace;
    
    
    public partial class free_to_read : XTypedElement, IXMetaData {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName contenttypeXName = System.Xml.Linq.XName.Get("content-type", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName end_dateXName = System.Xml.Linq.XName.Get("end_date", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName idXName = System.Xml.Linq.XName.Get("id", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName specificuseXName = System.Xml.Linq.XName.Get("specific-use", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName start_dateXName = System.Xml.Linq.XName.Get("start_date", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName @baseXName = System.Xml.Linq.XName.Get("base", "http://www.w3.org/XML/1998/namespace");
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("free_to_read", "http://www.niso.org/schemas/ali/1.0/");
        
		public static explicit operator free_to_read(XElement xe) { return XTypedServices.ToXTypedElement<free_to_read>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public free_to_read() {
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string contenttype {
            get {
                XAttribute x = this.Attribute(contenttypeXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(contenttypeXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string end_date {
            get {
                XAttribute x = this.Attribute(end_dateXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(end_dateXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string id {
            get {
                XAttribute x = this.Attribute(idXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
            set {
                this.SetAttribute(idXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string specificuse {
            get {
                XAttribute x = this.Attribute(specificuseXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(specificuseXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string start_date {
            get {
                XAttribute x = this.Attribute(start_dateXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(start_dateXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual System.Uri @base {
            get {
                XAttribute x = this.Attribute(@baseXName);
                return XTypedServices.ParseValue<System.Uri>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri).Datatype);
            }
            set {
                this.SetAttribute(@baseXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri).Datatype);
            }
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
        
        public void Save(string xmlFile) {
            XTypedServices.Save(xmlFile, Untyped);
        }
        
        public void Save(System.IO.TextWriter tw) {
            XTypedServices.Save(tw, Untyped);
        }
        
        public void Save(System.Xml.XmlWriter xmlWriter) {
            XTypedServices.Save(xmlWriter, Untyped);
        }
        
        public static free_to_read Load(string xmlFile) {
            return XTypedServices.Load<free_to_read>(xmlFile);
        }
        
        public static free_to_read Load(System.IO.TextReader xmlFile) {
            return XTypedServices.Load<free_to_read>(xmlFile);
        }
        
        public static free_to_read Parse(string xml) {
            return XTypedServices.Parse<free_to_read>(xml);
        }
        
        public override XTypedElement Clone() {
            return XTypedServices.CloneXTypedElement<free_to_read>(this);
        }
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return ContentModelEntity.Default;
        }
    }
    
    public partial class license_ref : XTypedElement, IXMetaData {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName contenttypeXName = System.Xml.Linq.XName.Get("content-type", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName idXName = System.Xml.Linq.XName.Get("id", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName specificuseXName = System.Xml.Linq.XName.Get("specific-use", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName start_dateXName = System.Xml.Linq.XName.Get("start_date", "");
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly System.Xml.Linq.XName @baseXName = System.Xml.Linq.XName.Get("base", "http://www.w3.org/XML/1998/namespace");
        
        private static readonly System.Xml.Linq.XName xName = System.Xml.Linq.XName.Get("license_ref", "http://www.niso.org/schemas/ali/1.0/");
        
		public static explicit operator license_ref(XElement xe) { return XTypedServices.ToXTypedElement<license_ref>(xe,LinqToXsdTypeManager.Instance as ILinqToXsdTypeManager); }
        
        public license_ref() {
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string contenttype {
            get {
                XAttribute x = this.Attribute(contenttypeXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(contenttypeXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string id {
            get {
                XAttribute x = this.Attribute(idXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
            set {
                this.SetAttribute(idXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Id).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string specificuse {
            get {
                XAttribute x = this.Attribute(specificuseXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(specificuseXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual string start_date {
            get {
                XAttribute x = this.Attribute(start_dateXName);
                return XTypedServices.ParseValue<string>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
            set {
                this.SetAttribute(start_dateXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).Datatype);
            }
        }
        
        /// <summary>
        /// <para>
        /// Occurrence: optional
        /// </para>
        /// </summary>
        public virtual System.Uri @base {
            get {
                XAttribute x = this.Attribute(@baseXName);
                return XTypedServices.ParseValue<System.Uri>(x, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri).Datatype);
            }
            set {
                this.SetAttribute(@baseXName, value, XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri).Datatype);
            }
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
        
        public void Save(string xmlFile) {
            XTypedServices.Save(xmlFile, Untyped);
        }
        
        public void Save(System.IO.TextWriter tw) {
            XTypedServices.Save(tw, Untyped);
        }
        
        public void Save(System.Xml.XmlWriter xmlWriter) {
            XTypedServices.Save(xmlWriter, Untyped);
        }
        
        public static license_ref Load(string xmlFile) {
            return XTypedServices.Load<license_ref>(xmlFile);
        }
        
        public static license_ref Load(System.IO.TextReader xmlFile) {
            return XTypedServices.Load<license_ref>(xmlFile);
        }
        
        public static license_ref Parse(string xml) {
            return XTypedServices.Parse<license_ref>(xml);
        }
        
        public override XTypedElement Clone() {
            return XTypedServices.CloneXTypedElement<license_ref>(this);
        }
        
        ContentModelEntity IXMetaData.GetContentModel() {
            return ContentModelEntity.Default;
        }
    }
    
    public class LinqToXsdTypeManager : ILinqToXsdTypeManager {
        
        private static Dictionary<System.Xml.Linq.XName, System.Type> elementDictionary = new Dictionary<System.Xml.Linq.XName, System.Type>();
        
        private static XmlSchemaSet schemaSet;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static LinqToXsdTypeManager typeManagerSingleton = new LinqToXsdTypeManager();
        
        static LinqToXsdTypeManager() {
            BuildElementDictionary();
        }
        
        private LinqToXsdTypeManager() {
        }
        
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
        
        Dictionary<System.Xml.Linq.XName, System.Type> ILinqToXsdTypeManager.GlobalTypeDictionary {
            get {
                return XTypedServices.EmptyDictionary;
            }
        }
        
        Dictionary<System.Xml.Linq.XName, System.Type> ILinqToXsdTypeManager.GlobalElementDictionary {
            get {
                return elementDictionary;
            }
        }
        
        Dictionary<System.Type, System.Type> ILinqToXsdTypeManager.RootContentTypeMapping {
            get {
                return XTypedServices.EmptyTypeMappingDictionary;
            }
        }
        
        public static LinqToXsdTypeManager Instance {
            get {
                return typeManagerSingleton;
            }
        }
        
        private static void BuildElementDictionary() {
            elementDictionary.Add(System.Xml.Linq.XName.Get("free_to_read", "http://www.niso.org/schemas/ali/1.0/"), typeof(global::www.niso.org.schemas.ali.free_to_read));
            elementDictionary.Add(System.Xml.Linq.XName.Get("license_ref", "http://www.niso.org/schemas/ali/1.0/"), typeof(global::www.niso.org.schemas.ali.license_ref));
        }
        
        protected internal static void AddSchemas(XmlSchemaSet schemas) {
            schemas.Add(schemaSet);
        }
        
        public static System.Type GetRootType() {
            return elementDictionary[System.Xml.Linq.XName.Get("free_to_read", "http://www.niso.org/schemas/ali/1.0/")];
        }
    }
    
    public partial class XRootNamespace {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XDocument doc;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;
        

		public free_to_read free_to_read {  get {return rootObject as free_to_read; } }

		public license_ref license_ref {  get {return rootObject as license_ref; } }
        
        private XRootNamespace() {
        }
        
        public XRootNamespace(free_to_read root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
        }
        
        public XRootNamespace(license_ref root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
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
    }
    
    public partial class XRoot {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XDocument doc;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private XTypedElement rootObject;
        

		public global::www.niso.org.schemas.ali.free_to_read free_to_read {  get {return rootObject as global::www.niso.org.schemas.ali.free_to_read; } }

		public global::www.niso.org.schemas.ali.license_ref license_ref {  get {return rootObject as global::www.niso.org.schemas.ali.license_ref; } }
        
        private XRoot() {
        }
        
        public XRoot(global::www.niso.org.schemas.ali.free_to_read root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
        }
        
        public XRoot(global::www.niso.org.schemas.ali.license_ref root) {
            this.doc = new XDocument(root.Untyped);
            this.rootObject = root;
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
    }
}
namespace www.w3.org.XML.Item1998.@namespace {
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
    using www.niso.org.schemas.ali;
    
    
    public sealed class lang {
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static Xml.Schema.Linq.SimpleTypeValidator TypeDefinition = new Xml.Schema.Linq.UnionSimpleTypeValidator(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyAtomicType), null, new Xml.Schema.Linq.SimpleTypeValidator[] {
                    new Xml.Schema.Linq.AtomicSimpleTypeValidator(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Language), null),
                    new Xml.Schema.Linq.AtomicSimpleTypeValidator(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), new Xml.Schema.Linq.RestrictionFacets(((Xml.Schema.Linq.RestrictionFlags)(16)), new object[] {
                                    ""}, 0, 0, null, null, 0, null, null, 0, null, 0, XmlSchemaWhiteSpace.Preserve))});
        
        private lang() {
        }
    }
}

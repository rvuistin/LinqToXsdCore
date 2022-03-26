//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Xml;
using System.Xml.Schema;

namespace Xml.Fxt
{
    // Lazy transformation object
    public class ExtractType : IFxtTransformation
    {
        internal XmlSchemaElement element;

        public void Run()
        {
            element.SchemaType.Name = element.Name;
            element.XmlSchema().Add(element.SchemaType);
            element.SchemaType = null;
            element.SchemaTypeName =
                new XmlQualifiedName(
                    element.Name,
                    element.XmlSchema().TargetNamespace);
        }
    }
}
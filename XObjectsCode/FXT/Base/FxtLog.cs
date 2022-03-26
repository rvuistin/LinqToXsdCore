//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace Xml.Fxt
{
    public class FxtLog
    {
        public List<FxtAnnotation> AtType(XmlQualifiedName n)
        {
            if (!aTypes.ContainsKey(n))
                aTypes.Add(n, new List<FxtAnnotation>());
            return aTypes[n];
        }

        public List<FxtAnnotation> AtElement(XmlQualifiedName n)
        {
            if (!aElements.ContainsKey(n))
                aElements.Add(n, new List<FxtAnnotation>());
            return aElements[n];
        }

        public List<FxtAnnotation> AtAttribute(XmlQualifiedName n)
        {
            if (!aAttributes.ContainsKey(n))
                aAttributes.Add(n, new List<FxtAnnotation>());
            return aAttributes[n];
        }

        public List<FxtAnnotation> AtObject(XmlSchemaObject o)
        {
            if (!aObjects.ContainsKey(o))
                aObjects.Add(o, new List<FxtAnnotation>());
            return aObjects[o];
        }

        Dictionary<XmlQualifiedName, List<FxtAnnotation>> aTypes =
            new Dictionary<XmlQualifiedName, List<FxtAnnotation>>();

        Dictionary<XmlQualifiedName, List<FxtAnnotation>> aElements =
            new Dictionary<XmlQualifiedName, List<FxtAnnotation>>();

        Dictionary<XmlQualifiedName, List<FxtAnnotation>> aAttributes =
            new Dictionary<XmlQualifiedName, List<FxtAnnotation>>();

        Dictionary<XmlSchemaObject, List<FxtAnnotation>> aObjects =
            new Dictionary<XmlSchemaObject, List<FxtAnnotation>>();
    }
}
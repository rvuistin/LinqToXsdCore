//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Xml;

namespace Xml.Fxt
{
    public class FxtElementClashException : FxtException
    {
        public FxtElementClashException(XmlQualifiedName name) : base()
        {
        }
    }
}
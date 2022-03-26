//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Xml.Schema.Linq.Extensions;

namespace Xml.Schema.Linq.CodeGen
{
    internal abstract partial class ClrTypeInfo
    {
        //Names
        internal string clrtypeName;
        internal string clrtypeNs;
        internal string schemaName;
        internal string schemaNs;
        internal string clrFullTypeName;

        internal string contentModelRegEx;

        internal XmlSchemaObject baseType;
        internal string baseTypeClrName;
        internal string baseTypeClrNs;

        internal ClrTypeInfo Parent { get; set; }

        //Type properties 
        protected ClrTypeFlags clrTypeFlags;
        internal SchemaOrigin typeOrigin;

        //Intellisense type information
        protected List<ClrAnnotation> annotations;

        public ClrTypeInfo()
        {
            Init();
        }

        /// <summary>
        /// If this instance is nested under another <see cref="ClrTypeInfo"/> instance,
        /// this method will resolve all the parent references and return a string that can be used
        /// in a fully-qualified type reference string.
        /// <para>Intended for type references that are themselves defined inside another.</para>
        /// </summary>
        /// <param name="includeNs">Optionally set to true to include the namespace.</param>
        /// <returns></returns>
        internal string GetNestedTypeScopedResolutionString(bool includeNs = true)
        {
            var scopeSb = includeNs ? new StringBuilder(this.clrtypeNs) : new StringBuilder();

            var theParent = Parent;
            var iterationCount = 0;
            do {
                if (theParent?.clrtypeName.IsNullOrEmpty() ?? true) {
                    if (iterationCount == 0) goto appendThis;
                    else continue;
                }

                scopeSb.Append("." + theParent.clrtypeName);
                theParent = theParent.Parent;
                iterationCount++;
                appendThis:
                if (theParent == null) {
                    // add this instance typename after exhausting all parent refs
                    scopeSb.Append("." + this.clrtypeName);
                }
            } while (theParent != null);

            // this happens when both this.clrTypeName is null and Parent is null
            if (scopeSb.Length == 1) return string.Empty;

            return scopeSb.ToString();
        }

        private void Init()
        {
            clrtypeName = null;
            clrtypeNs = null;
            schemaName = null;
            schemaNs = null;
            baseType = null;

            clrTypeFlags = ClrTypeFlags.None;
            typeOrigin = SchemaOrigin.None;

            annotations = new List<ClrAnnotation>();
        }

        internal bool IsDerived
        {
            get { return baseType != null; }
        }

        internal XmlQualifiedName BaseTypeName
        {
            get
            {
                if (baseType == null)
                {
                    return XmlQualifiedName.Empty;
                }

                XmlSchemaType schemaType = baseType as XmlSchemaType;
                if (schemaType != null)
                {
                    return schemaType.QualifiedName;
                }
                else
                {
                    XmlSchemaElement schemaElement = baseType as XmlSchemaElement; //For subst groups
                    return schemaElement.QualifiedName;
                }
            }
        }

        internal bool IsAbstract
        {
            get { return (clrTypeFlags & ClrTypeFlags.IsAbstract) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.IsAbstract;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.IsAbstract;
                }
            }
        }

        internal bool IsSealed
        {
            get { return (clrTypeFlags & ClrTypeFlags.IsSealed) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.IsSealed;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.IsSealed;
                }
            }
        }

        internal bool IsRoot
        {
            get { return (clrTypeFlags & ClrTypeFlags.IsRoot) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.IsRoot;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.IsRoot;
                }
            }
        }

        internal bool IsNested
        {
            get { return (clrTypeFlags & ClrTypeFlags.IsNested) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.IsNested;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.IsNested;
                }
            }
        }

        internal bool InlineBaseType
        {
            get { return (clrTypeFlags & ClrTypeFlags.InlineBaseType) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.InlineBaseType;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.InlineBaseType;
                }
            }
        }

        internal bool IsSubstitutionHead
        {
            get { return (clrTypeFlags & ClrTypeFlags.IsSubstitutionHead) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.IsSubstitutionHead;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.IsSubstitutionHead;
                }
            }
        }

        internal bool HasElementWildCard
        {
            get { return (clrTypeFlags & ClrTypeFlags.HasElementWildCard) != 0; }
            set
            {
                if (value)
                {
                    clrTypeFlags |= ClrTypeFlags.HasElementWildCard;
                }
                else
                {
                    clrTypeFlags &= ~ClrTypeFlags.HasElementWildCard;
                }
            }
        }

        internal bool IsRootElement
        {
            get { return typeOrigin == SchemaOrigin.Element; }
        }


        internal bool IsSubstitutionMember()
        {
            //types whose origin is element, If they have a base type its from being a member of a subst group
            if (typeOrigin == SchemaOrigin.Element && baseType != null && !IsHeadAnyType())
            {
                //skip if the head element is xs:anyType
                return true;
            }

            return false;
        }

        internal virtual bool IsWrapper
        {
            get { return false; }
        }

        internal virtual bool HasBaseContentType
        {
            //For wrappers that are substitutionGroup members, if the type is the same as that of the head, this property returns true
            get { return false; }
        }

        internal List<ClrAnnotation> Annotations
        {
            get { return annotations; }
        }

        internal string ContentModelRegEx
        {
            get { return contentModelRegEx; }

            set { contentModelRegEx = value; }
        }

        private bool IsHeadAnyType()
        {
            Debug.Assert(baseType != null);
            XmlSchemaElement headElem = baseType as XmlSchemaElement;
            Debug.Assert(headElem != null);
            return headElem.ElementSchemaType.TypeCode == XmlTypeCode.Item;
        }

        internal virtual FSM CreateFSM(StateNameSource stateNames)
        {
            throw new InvalidOperationException();
        }

        public override string ToString() => this.clrtypeName;
    }
}

//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Xml.Schema.Linq.CodeGen
{
    internal class ClrWrapperTypeInfo : ClrTypeInfo
    {
        ClrTypeReference innerType;
        internal string fixedDefaultValue;
        bool hasBaseContentType;

        internal ClrWrapperTypeInfo()
        {
        }

        internal ClrWrapperTypeInfo(bool hasBaseContentType)
        {
            this.hasBaseContentType = hasBaseContentType;
        }

        internal override bool IsWrapper
        {
            get { return true; }
        }

        internal override bool HasBaseContentType
        {
            get { return hasBaseContentType; }
        }

        internal ClrTypeReference InnerType
        {
            get { return innerType; }
            set { innerType = value; }
        }

        internal string FixedValue
        {
            get
            {
                if ((clrTypeFlags & ClrTypeFlags.HasFixedValue) != 0) return fixedDefaultValue;
                else return null;
            }
            set
            {
                if (value != null)
                {
                    clrTypeFlags |= ClrTypeFlags.HasFixedValue;
                    fixedDefaultValue = value;
                }
            }
        }


        internal string DefaultValue
        {
            get
            {
                if ((clrTypeFlags & ClrTypeFlags.HasDefaultValue) != 0) return fixedDefaultValue;
                else return null;
            }
            set
            {
                if (value != null)
                {
                    clrTypeFlags |= ClrTypeFlags.HasDefaultValue;
                    fixedDefaultValue = value;
                }
            }
        }
    }
}

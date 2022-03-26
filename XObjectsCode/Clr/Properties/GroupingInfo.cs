//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Xml.Schema.Linq.CodeGen
{
    internal partial class GroupingInfo : ContentInfo
    {
        ContentModelType contentModelType;
        GroupingFlags groupingFlags;

        internal GroupingInfo(ContentModelType cmType, Occurs occursInSchema)
        {
            this.contentModelType = cmType;
            this.occursInSchema = occursInSchema;
            this.contentType = ContentType.Grouping;
            if ((int) occursInSchema > (int) Occurs.ZeroOrOne)
            {
                groupingFlags |= GroupingFlags.Repeating;
            }
        }

        internal bool IsRepeating
        {
            get { return (groupingFlags & GroupingFlags.Repeating) != 0; }
            set
            {
                if (value)
                {
                    groupingFlags |= GroupingFlags.Repeating;
                }
                else
                {
                    groupingFlags &= ~GroupingFlags.Repeating;
                }
            }
        }

        internal bool HasChildGroups
        {
            get { return (groupingFlags & GroupingFlags.HasChildGroups) != 0; }
            set
            {
                if (value)
                {
                    groupingFlags |= GroupingFlags.HasChildGroups;
                }
                else
                {
                    groupingFlags &= ~GroupingFlags.HasChildGroups;
                }
            }
        }

        internal bool HasRepeatingGroups
        {
            get { return (groupingFlags & GroupingFlags.HasRepeatingGroups) != 0; }
            set
            {
                if (value)
                {
                    groupingFlags |= GroupingFlags.HasRepeatingGroups;
                }
                else
                {
                    groupingFlags &= ~GroupingFlags.HasRepeatingGroups;
                }
            }
        }

        internal bool IsNested
        {
            get { return (groupingFlags & GroupingFlags.Nested) != 0; }
            set
            {
                if (value)
                {
                    groupingFlags |= GroupingFlags.Nested;
                }
                else
                {
                    groupingFlags &= ~GroupingFlags.Nested;
                }
            }
        }

        internal bool HasRecurrentElements
        {
            get { return (groupingFlags & GroupingFlags.HasRecurrentElements) != 0; }
            set
            {
                if (value)
                {
                    groupingFlags |= GroupingFlags.HasRecurrentElements;
                }
                else
                {
                    groupingFlags &= ~GroupingFlags.HasRecurrentElements;
                }
            }
        }

        internal bool IsComplex
        {
            get
            {
                return
                    HasChildGroups | IsRepeating |
                    HasRecurrentElements; //Earlier we used to disable DML for the repeating and nested part of the content model
                //Now, we turn the whole content model to AppendOnly mode
            }
        }

        internal ContentModelType ContentModelType
        {
            get { return contentModelType; }
        }

        public override string ToString() => $"{this.contentModelType} {base.ToString()}";
    }
}

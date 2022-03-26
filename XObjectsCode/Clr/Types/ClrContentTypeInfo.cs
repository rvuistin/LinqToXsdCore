//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Xml.Schema.Linq.CodeGen
{
    internal class ClrContentTypeInfo : ClrTypeInfo
    {
        //Group/Properties
        internal ContentInfo lastTypeMember;

        //Nested types
        internal List<ClrTypeInfo> nestedTypes;

        internal ClrContentTypeInfo()
        {
        }

        internal IEnumerable<ContentInfo> Content
        {
            get
            {
                ContentInfo current = lastTypeMember;
                while (current != null)
                {
                    current = current.nextSibling;
                    yield return current;
                    if (current == lastTypeMember)
                    {
                        yield break;
                    }
                }
            }
        }

        internal void AddMember(ContentInfo member)
        {
            if (lastTypeMember == null)
            {
                member.nextSibling = member; //Point to the same item as first
            }
            else
            {
                member.nextSibling = lastTypeMember.nextSibling;
                lastTypeMember.nextSibling = member;
            }

            lastTypeMember = member;
        }

        internal List<ClrTypeInfo> NestedTypes
        {
            get
            {
                if (nestedTypes == null)
                {
                    nestedTypes = new List<ClrTypeInfo>();
                }

                return nestedTypes;
            }
        }

        internal override FSM CreateFSM(StateNameSource stateNames)
        {
            //Should have only one top-level grouping content info
            foreach (ContentInfo content in Content)
            {
                GroupingInfo group = content as GroupingInfo;
                if (group != null)
                {
                    FSM fsm = group.MakeFSM(stateNames);
                    return fsm;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {{ {string.Join(", ", this.Content.Select(x => x.ToString()))} }}";
        }
    }
}

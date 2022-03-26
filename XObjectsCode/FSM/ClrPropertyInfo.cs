//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Xml.Schema.Linq.CodeGen
{
    internal partial class ClrPropertyInfo : ClrBasePropertyInfo
    {
        internal override FSM MakeFSM(StateNameSource stateNames)
        {
            //Create a simple fsm with (0,(schemaName,1),{1})
            Dictionary<int, Transitions> transitions = new Dictionary<int, Transitions>();
            int start = stateNames.Next();
            int end = stateNames.Next();
            Transitions trans = new Transitions();

            if (this.IsSubstitutionHead)
            {
                foreach (XmlSchemaElement element in SubstitutionMembers)
                {
                    trans.Add(XName.Get(element.QualifiedName.Name, element.QualifiedName.Namespace), end);
                }
            }
            else
            {
                trans.Add(XName.Get(schemaName, PropertyNs), end);
            }

            transitions.Add(start, trans);
            return ImplementFSMCardinality(new FSM(start, new Set<int>(end), transitions), stateNames);
        }
    }
}
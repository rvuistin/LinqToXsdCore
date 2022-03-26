//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;

namespace Xml.Schema.Linq.CodeGen
{
    internal partial class ClrWildCardPropertyInfo : ClrBasePropertyInfo
    {
        internal override FSM MakeFSM(StateNameSource stateNames)
        {
            Dictionary<int, Transitions> transitions = new Dictionary<int, Transitions>();
            int start = stateNames.Next();
            int end = stateNames.Next();
            transitions.Add(start,
                new Transitions(new SingleTransition(new WildCard(this.Namespaces, this.TargetNamespace), end)));
            FSM fsm = new FSM(start, new Set<int>(end), transitions);

            return ImplementFSMCardinality(fsm, stateNames);
        }
    }
}
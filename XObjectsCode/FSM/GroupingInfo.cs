//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Xml.Schema.Linq.CodeGen
{
    internal partial class GroupingInfo
    {
        internal override FSM MakeFSM(StateNameSource stateNames)
        {
            FSM fsm = null;
            switch (this.contentModelType)
            {
                case ContentModelType.Sequence:
                    fsm = MakeSequenceFSM(stateNames);
                    break;
                case ContentModelType.Choice:
                    fsm = MakeChoiceFSM(stateNames);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return ImplementFSMCardinality(fsm, stateNames);
        }

        private FSM MakeSequenceFSM(StateNameSource stateNames)
        {
            FSM fsm = null;
            Set<int> fsmAccept = null;

            foreach (ContentInfo child in Children)
            {
                FSM currFsm = child.MakeFSM(stateNames);

                if (fsm == null)
                {
                    fsm = currFsm;
                    fsmAccept = currFsm.Accept;
                }
                else
                {
                    int currStart = currFsm.Start;
                    foreach (int oldFinalState in fsmAccept)
                    {
                        FSM.CloneTransitions(currFsm, currStart, fsm, oldFinalState);
                    }

                    fsm.AddTransitions(currFsm);
                    //clear old final states only if the initial state of currFsm is not a final state in currFsm
                    if (!currFsm.Accept.Contains(currStart)) fsmAccept.Clear();
                    Set<int> currAccept = currFsm.Accept;
                    foreach (int state in currAccept) fsmAccept.Add(state);
                }
            }

            return fsm;
        }

        private FSM MakeChoiceFSM(StateNameSource stateNames)
        {
            FSM fsm = null;
            int fsmStart = FSM.InvalidState;
            Set<int> fsmAccept = null;

            foreach (ContentInfo child in Children)
            {
                FSM currFsm = child.MakeFSM(stateNames);

                if (fsm == null)
                {
                    //first node
                    fsm = currFsm;
                    fsmStart = currFsm.Start;
                    fsmAccept = currFsm.Accept;
                }
                else
                {
                    //Merge the start states
                    FSM.CloneTransitions(currFsm, currFsm.Start, fsm, fsmStart);
                    //Copy other transitions
                    fsm.AddTransitions(currFsm);
                    //update final states
                    if (currFsm.isAccept(currFsm.Start)) fsmAccept.Add(fsmStart);
                    foreach (int state in currFsm.Accept) fsmAccept.Add(state);
                }
            }

            return fsm;
        }
    }
}
//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.CodeDom;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Xml.Schema.Linq.CodeGen
{
    internal class FSMCodeDomHelper
    {
        internal static void CreateFSMStmt(FSM fsm, CodeStatementCollection stmts)
        {
            //First create: Dictionary<int, Transitions> transitions = new Dictionary<int,Transitions>();
            //Then create: transitions.Add(0, new Transitions(...));
            //Last: fsm = new DFA(start, new Set<int>(end), transitions);
            CodeTypeReference typeRef =
                CodeDomHelper.CreateGenericTypeReference("Dictionary", new string[] {Constants.Int, "Transitions"});
            stmts.Add(new CodeVariableDeclarationStatement(typeRef, Constants.TransitionsVar,
                new CodeObjectCreateExpression(typeRef, new CodeExpression[] { })));

            //Then add all transitions
            Set<int> visited = new Set<int>();
            AddTransitions(fsm, fsm.Start, stmts, visited);

            //Clean up accept states
            Set<int> reachableAccept = new Set<int>();
            foreach (int state in fsm.Accept)
            {
                if (visited.Contains(state)) reachableAccept.Add(state);
            }

            stmts.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(Constants.FSMMember),
                new CodeObjectCreateExpression(new CodeTypeReference(Constants.FSMClass),
                    new CodePrimitiveExpression(fsm.Start),
                    CreateSetCreateExpression(reachableAccept),
                    new CodeVariableReferenceExpression(Constants.TransitionsVar))));
        }

        internal static void AddTransitions(FSM fsm, int state, CodeStatementCollection stmts, Set<int> visited)
        {
            if (visited.Contains(state)) return;
            else visited.Add(state);

            Transitions currTrans = null;
            fsm.Trans.TryGetValue(state, out currTrans);
            if (currTrans == null || currTrans.Count == 0) return;

            CreateAddTransitionStmts(fsm, stmts, state, currTrans, visited);
        }

        internal static void CreateAddTransitionStmts(FSM fsm,
            CodeStatementCollection stmts,
            int state,
            Transitions currTrans,
            Set<int> visited)
        {
            Set<int> subStates = new Set<int>();
            CodeExpression[] initializers = new CodeExpression[currTrans.Count];

            int index = 0;
            if (currTrans.nameTransitions != null)
                foreach (KeyValuePair<XName, int> s1Trans in currTrans.nameTransitions)
                {
                    initializers[index++] = CreateSingleTransitionExpr(CreateXNameExpr(s1Trans.Key), s1Trans.Value);
                    subStates.Add(s1Trans.Value);
                }

            if (currTrans.wildCardTransitions != null)
                foreach (KeyValuePair<WildCard, int> s1Trans in currTrans.wildCardTransitions)
                {
                    initializers[index++] = CreateSingleTransitionExpr(CreateWildCardExpr(s1Trans.Key), s1Trans.Value);
                    subStates.Add(s1Trans.Value);
                }


            stmts.Add(CodeDomHelper.CreateMethodCall(new CodeVariableReferenceExpression(Constants.TransitionsVar),
                "Add",
                new CodeExpression[]
                {
                    new CodePrimitiveExpression(state),
                    new CodeObjectCreateExpression("Transitions", initializers)
                }));

            //Recursively call AddTransitions on subsequent states
            foreach (int s in subStates) AddTransitions(fsm, s, stmts, visited);
        }


        internal static CodeExpression CreateSingleTransitionExpr(CodeExpression labelExpr, int nextState)
        {
            return new CodeObjectCreateExpression(
                Constants.SingleTrans,
                labelExpr,
                new CodePrimitiveExpression(nextState));
        }

        internal static CodeExpression CreateXNameExpr(XName name)
        {
            return CodeDomHelper.CreateMethodCall(new CodeTypeReferenceExpression(Constants.XNameType),
                "Get",
                new CodeExpression[]
                {
                    new CodePrimitiveExpression(name.LocalName),
                    new CodePrimitiveExpression(name.Namespace.NamespaceName)
                });
        }

        internal static CodeExpression CreateWildCardExpr(WildCard any)
        {
            return new CodeObjectCreateExpression(
                Constants.WildCard,
                new CodeExpression[]
                {
                    new CodePrimitiveExpression(any.NsList.Namespaces),
                    new CodePrimitiveExpression(any.NsList.TargetNamespace)
                }
            );
        }

        internal static CodeObjectCreateExpression CreateSetCreateExpression(Set<int> set)
        {
            CodeObjectCreateExpression createSet =
                new CodeObjectCreateExpression(
                    CodeDomHelper.CreateGenericTypeReference("Set", new string[] {Constants.Int}));

            CodeExpressionCollection parameters = createSet.Parameters;
            if (set.Count == 1)
            {
                foreach (int i in set)
                {
                    parameters.Add(new CodePrimitiveExpression(i));
                }
            }
            else if (set.Count > 1)
            {
                CodeArrayCreateExpression array = new CodeArrayCreateExpression();
                array.CreateType = CodeDomHelper.CreateTypeReference(Constants.Int);

                CodeExpressionCollection initializers = array.Initializers;
                foreach (int i in set)
                {
                    initializers.Add(new CodePrimitiveExpression(i));
                }

                parameters.Add(array);
            }

            return createSet;
        }
    }
}
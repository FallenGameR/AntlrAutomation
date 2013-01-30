// -----------------------------------------------------------------------
// <copyright file="AutomationTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
    using Antlr.Runtime;
    using Antlr.Runtime.Tree;

    [Serializable]
    public class AutomationTree : CommonTree
    {
        public AutomationTree()
            : base()
        {
        }

        public AutomationTree(IToken token)
            : base(token)
        {
        }

        public AutomationTree(CommonTree node)
            : base(node)
        {
        }
    }
}

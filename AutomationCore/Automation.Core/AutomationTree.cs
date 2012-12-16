// -----------------------------------------------------------------------
// <copyright file="AutomationTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Antlr.Runtime.Tree;
    using Antlr.Runtime;

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

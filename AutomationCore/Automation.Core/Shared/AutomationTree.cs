// -----------------------------------------------------------------------
// <copyright file="AutomationTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq.Expressions;
    using Antlr.Runtime;
    using Antlr.Runtime.Tree;

    [Serializable]
    public class AutomationTree : CommonTree, IDynamicMetaObjectProvider
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

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicMetaTree(parameter, this);
        }

        public IEnumerable<AutomationTree> Find(string name)
        {
            if (this.Children == null)
            {
                yield break;
            }

            foreach (var child in this.Children)
            {
                if (StringComparer.OrdinalIgnoreCase.Equals(child.Text, name))
                {
                    yield return (AutomationTree)child;
                }
            }
        }
    }
}

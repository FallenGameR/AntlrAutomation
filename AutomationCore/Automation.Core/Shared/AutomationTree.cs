// -----------------------------------------------------------------------
// <copyright file="AutomationTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
<<<<<<< HEAD
=======
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq.Expressions;
>>>>>>> d37def1... Failing test for AutomationTreeTests
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
<<<<<<< HEAD
=======

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicMetaTree(parameter, this);
        }

        public IEnumerable<AutomationTree> Find(string name)
        {
            throw new NotImplementedException();
        }
>>>>>>> d37def1... Failing test for AutomationTreeTests
    }
}

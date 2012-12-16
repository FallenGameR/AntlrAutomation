// -----------------------------------------------------------------------
// <copyright file="AutomationAdapter.cs" company="">
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

    public class AutomationAdaptor: CommonTreeAdaptor
    {
        public override object Create(IToken token)
        {
            return new AutomationTree(token);
        }
    }
}

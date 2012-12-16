// -----------------------------------------------------------------------
// <copyright file="AutomationAdapter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using Antlr.Runtime;
    using Antlr.Runtime.Tree;

    public class AutomationAdaptor: CommonTreeAdaptor
    {
        public override object Create(IToken token)
        {
            return new AutomationTree(token);
        }
    }
}

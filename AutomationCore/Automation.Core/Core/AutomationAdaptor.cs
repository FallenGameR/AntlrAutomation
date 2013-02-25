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

        public override object ErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException ex)
        {
            return new AutomationErrorNode(input, start, stop, ex);
        }
    }
}

using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace Automation.Core
{
    [Serializable]
    public class AutomationErrorNode : AutomationTree
    {
        private bool isNil;
        private int type;
        private string text;
        private string toString;

        public AutomationErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException ex)
        {
            var handler = new CommonErrorNode(input, start, stop, ex);
            this.isNil = handler.IsNil;
            this.type = handler.Type;
            this.text = handler.Text;
            this.toString = handler.ToString();
        }

        public override bool IsNil
        {
            get { return this.isNil; }
        }

        public override string Text
        {
            get { return this.text; }
        }

        public override int Type
        {
            get { return this.type; }
        }

        public override string ToString()
        {
            return this.toString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class BeginningOfLineGenerator : IGenerator
    {
        private readonly int beginningOfLineType;

        private BeginningOfLineGenerator(int beginningOfLineType)
        {
            this.beginningOfLineType = beginningOfLineType;
        }

        public static IGenerator GetInstance(int beginningOfLineType)
        {
            return new BeginningOfLineGenerator(beginningOfLineType);
        }

        public bool IsTrigger(IToken token)
        {
            return token.CharPositionInLine == 0
                && !token.IsEof();
        }

        public IEnumerable<IToken> Generate(IToken token)
        {
            return new [] { new CommonToken(token)
            {
                Text = string.Empty,
                Type = this.beginningOfLineType,
                Channel = Lexer.DefaultTokenChannel,
                CharPositionInLine = 0,
            }};
        }
    }
}

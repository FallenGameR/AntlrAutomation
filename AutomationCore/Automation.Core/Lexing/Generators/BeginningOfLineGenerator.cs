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
        private readonly int channel;

        private BeginningOfLineGenerator(int beginningOfLineType, int channel)
        {
            this.beginningOfLineType = beginningOfLineType;
            this.channel = channel;
        }

        public static IGenerator GetInstance(int beginningOfLineType, int channel)
        {
            return new BeginningOfLineGenerator(beginningOfLineType, channel);
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
                Channel = this.channel,
                CharPositionInLine = 0,
            }};
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class IndentionGenerator: IGenerator
    {
        private readonly int indentType;
        private readonly int dedentType;
        private readonly int whitespaceType;
        private readonly int channelNumber;
        private readonly IndentionDetector detector;

        private IndentionGenerator(int indentType, int dedentType, int whitespaceType, int channel)
        {
            this.indentType = indentType;
            this.dedentType = dedentType;
            this.whitespaceType = whitespaceType;
            this.channelNumber = channel;
            this.detector = IndentionDetector.GetInstance();
        }

        public static IndentionGenerator GetInstance(int indentType, int dedentType, int whitespaceType, int channel)
        {
            return new IndentionGenerator(indentType, dedentType, whitespaceType, channel);
        }

        public bool IsTrigger(IToken token)
        {
            var isLeadingWhitespace = (token.Type == this.whitespaceType) && (token.CharPositionInLine == 0);
            return isLeadingWhitespace || token.IsEof();
        }

        public IEnumerable<IToken> Generate(IToken token)
        {
            return this.detector
                .Detect(this.GetPosition(token))
                .Select(ind => this.GenerateImaginaryToken(ind, token));
        }

        private IToken GenerateImaginaryToken(Indention indention, IToken original)
        {
            switch (indention)
            {
                case Indention.Indent:
                    return new CommonToken(original)
                    {
                        Text = "INDENT",
                        Type = this.indentType,
                        Channel = this.channelNumber,
                    };

                case Indention.Dedent:
                    return new CommonToken(original)
                    {
                        Text = "DEDENT",
                        Type = this.dedentType,
                        Channel = this.channelNumber,
                    };

                default:
                    throw new AutomationException("Unknown indentation: " + indention.ToString());
            }
        }

        private int GetPosition(IToken indentationToken)
        {
            if (indentationToken.IsEof())
            {
                // Indention is always 0 for EOF
                return 0;
            }
            else
            {
                // Indention for whitespace tokens must be calculated
                return indentationToken.CharPositionInLine + indentationToken.Text.AsEnumerable().Sum(c => this.GetWhitespaceLength(c));
            }
        }

        private int GetWhitespaceLength(char character)
        {
            switch (character)
            {
                case ' ': return 1;
                case '\t': return 4;
                default: throw new InvalidOperationException("Can't get whitespace length for char: " + ((int)character).ToString());
            }
        }
    }
}

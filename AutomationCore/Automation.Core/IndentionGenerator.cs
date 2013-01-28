using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class IndentionGenerator
    {
        private int whitespaceType = 32;
        private int indentType = 10;
        private int dedentType = 20;
        private IndentionDetector detector = IndentionDetector.GetInstance();


        public IEnumerable<IToken> Process(IToken token)
        {
            if (this.IsTrigger(token))
            {
                return this.detector
                .Detect(this.GetPosition(token))
                .Select(ind => this.GenerateImaginaryToken(ind, token));
            }
            else
            {
                return new IToken[0];
            }
        }

        private bool IsTrigger(IToken token)
        {
            var isLeadingWhitespace = (token.Type == this.whitespaceType) && (token.CharPositionInLine == 0);
            return isLeadingWhitespace || token.IsEof();
        }

        private IToken GenerateImaginaryToken(Indention indention, IToken original)
        {
            switch (indention)
            {
                case Indention.Indent:
                    return new CommonToken(original)
                    {
                        Type = this.indentType,
                        Text = "INDENT",
                        Channel = Lexer.DefaultTokenChannel
                    };

                case Indention.Dedent:
                    return new CommonToken(original)
                    {
                        Type = this.dedentType,
                        Text = "DEDENT",
                        Channel = Lexer.DefaultTokenChannel
                    };

                default:
                    throw new AutomationException("Unknown indentation: " + indention.ToString());
            }
        }

        public int GetPosition(IToken indentationToken)
        {
            // Indention is always 0 for EOF
            if (indentationToken.IsEof())
            {
                return 0;
            }

            // Indention for whitespace tokens must be calculated
            return indentationToken.CharPositionInLine + indentationToken.Text.AsEnumerable().Sum(c => this.GetWhitespaceLength(c));
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

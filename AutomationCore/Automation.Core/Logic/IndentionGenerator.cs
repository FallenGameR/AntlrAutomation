using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class IndentionGenerator
    {
        private readonly int indentType;
        private readonly int dedentType;
        private readonly int whitespaceType;
        private readonly int channelNumber;
        private readonly IndentionDetector detector;
        private readonly Queue<IToken> queuedTokens;

        private IndentionGenerator(int indentType, int dedentType, int whitespaceType, int channel)
        {
            this.indentType = indentType;
            this.dedentType = dedentType;
            this.whitespaceType = whitespaceType;
            this.channelNumber = channel;
            this.detector = IndentionDetector.GetInstance();
            this.queuedTokens = new Queue<IToken>();
        }

        public bool HasTokens
        {
            get { return this.queuedTokens.Any(); }
        }

        public static IndentionGenerator GetInstance(int indentType, int dedentType, int whitespaceType, int channel)
        {
            return new IndentionGenerator(indentType, dedentType, whitespaceType, channel);
        }

        public void Process(IToken token)
        {
            // Queue indentation tokens if needed
            if (this.IsTrigger(token))
            {
                var toEnqueue = this.detector
                    .Detect(this.GetPosition(token))
                    .Select(ind => this.GenerateImaginaryToken(ind, token));

                foreach (var generatedToken in toEnqueue)
                {
                    this.queuedTokens.Enqueue(generatedToken);
                }
            }

            // Preserve original token
            this.queuedTokens.Enqueue(token);
        }

        public IToken NextToken()
        {
            return this.queuedTokens.Dequeue();
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
                        Channel = channelNumber,
                    };

                case Indention.Dedent:
                    return new CommonToken(original)
                    {
                        Type = this.dedentType,
                        Text = "DEDENT",
                        Channel = channelNumber,
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

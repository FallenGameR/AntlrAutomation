using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class IndentionGeneratorTests
    {
        private const int any = 10;
        private const int indent = 20;
        private const int dedent = 30;
        private const int whitespace = 40;
        private const int channel = 50;

        private const int leadingPosition = 0;

        private const int anyPosition = 1;
        private const int anyLine = 2;
        private const int anyIndex = 3;
        private const int anyChannel = 4;

        private Emitter emitter;

        [TestInitialize]
        public void Initialize()
        {
            this.emitter = Emitter.GetInstance(IndentionGenerator.GetInstance(indent, dedent, whitespace, channel));
        }

        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            // HasTokens shows if there are tokens left in the queue
            Assert.IsFalse(emitter.HasTokens);

            // Process preserves original token in the queue
            emitter.Process(this.GetToken(any));
            Assert.IsTrue(emitter.HasTokens);

            // NextToken drains the queue
            Assert.AreEqual(any, emitter.NextToken().Type);
            Assert.IsFalse(emitter.HasTokens);

            // Process puts indention tokens on whitespaces 
            emitter.Process(this.GetToken(whitespace, " "));
            Assert.AreEqual(indent, emitter.NextToken().Type);
            Assert.IsTrue(emitter.HasTokens);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);
            Assert.IsFalse(emitter.HasTokens);

            // Process puts dedention tokens on whitespaces
            emitter.Process(this.GetToken(whitespace, "    "));
            emitter.Process(this.GetToken(whitespace, " "));
            Assert.AreEqual(indent, emitter.NextToken().Type);
            Assert.IsTrue(emitter.HasTokens);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);
            Assert.IsTrue(emitter.HasTokens);
            Assert.AreEqual(dedent, emitter.NextToken().Type);
            Assert.IsTrue(emitter.HasTokens);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);
            Assert.IsFalse(emitter.HasTokens);

            // Process puts dedention tokens on EOF
            emitter.Process(this.GetToken(Constant.Eof));
            Assert.AreEqual(dedent, emitter.NextToken().Type);
            Assert.IsTrue(emitter.HasTokens);
            Assert.AreEqual(Constant.Eof, emitter.NextToken().Type);
            Assert.IsFalse(emitter.HasTokens);
        }

        [TestMethod]
        public void Generated_INDENT_token_is_correct()
        {
            var leadingWhitespaceToken = new CommonToken
            {
                Type = whitespace,
                Text = " ",
                CharPositionInLine = leadingPosition,
                Line = anyLine,
                StartIndex = anyIndex,
                TokenIndex = anyIndex,
                StopIndex = anyIndex,
                Channel = anyChannel,
            };

            this.emitter.Process(leadingWhitespaceToken);
            var token = this.emitter.NextToken();

            Assert.AreEqual(indent, token.Type);
            Assert.AreEqual("INDENT", token.Text);
            Assert.AreEqual(channel, token.Channel);
            Assert.AreEqual(leadingPosition, token.CharPositionInLine);
            Assert.AreEqual(anyLine, token.Line);
            Assert.AreEqual(anyIndex, token.StartIndex);
            Assert.AreEqual(anyIndex, token.StopIndex);
            Assert.AreEqual(anyIndex, token.TokenIndex);
        }

        [TestMethod]
        public void Generated_DEDENT_token_is_correct()
        {
            var eofToken = new CommonToken
            {
                Type = Constant.Eof,
                Text = "EOF",
                CharPositionInLine = anyPosition,
                Line = anyLine,
                StartIndex = anyIndex,
                TokenIndex = anyIndex,
                StopIndex = anyIndex,
                Channel = anyChannel,
            };

            this.emitter.Process(this.GetToken(whitespace, " "));
            this.emitter.NextToken();
            this.emitter.NextToken();
            this.emitter.Process(eofToken);
            var token = this.emitter.NextToken();

            Assert.AreEqual(dedent, token.Type);
            Assert.AreEqual("DEDENT", token.Text);
            Assert.AreEqual(channel, token.Channel);
            Assert.AreEqual(anyPosition, token.CharPositionInLine);
            Assert.AreEqual(anyLine, token.Line);
            Assert.AreEqual(anyIndex, token.StartIndex);
            Assert.AreEqual(anyIndex, token.StopIndex);
            Assert.AreEqual(anyIndex, token.TokenIndex);
        }

        [TestMethod]
        public void Position_uses_1_for_spaces_and_4_for_tabs()
        {
            var spaceLength = 1;
            var tabLength = 4;

            this.emitter.Process(this.GetToken(whitespace, new string(' ', tabLength - spaceLength)));
            Assert.AreEqual(indent, emitter.NextToken().Type);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);

            this.emitter.Process(this.GetToken(whitespace, "\t"));
            Assert.AreEqual(indent, emitter.NextToken().Type);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);

            this.emitter.Process(this.GetToken(whitespace, new string(' ', tabLength + spaceLength)));
            Assert.AreEqual(indent, emitter.NextToken().Type);
            Assert.AreEqual(whitespace, emitter.NextToken().Type);
        }

        private IToken GetToken(int type, string text = "")
        {
            return new CommonToken(type, text) { CharPositionInLine = 0 };
        }
    }
}

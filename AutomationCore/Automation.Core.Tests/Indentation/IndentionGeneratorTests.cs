﻿using System;
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

        private IndentionGenerator generator;

        [TestInitialize]
        public void Initialize()
        {
            this.generator = IndentionGenerator.GetInstance(indent, dedent, whitespace, channel);
        }

        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            // HasTokens shows if there are tokens left in the queue
            Assert.IsFalse(generator.HasTokens);

            // Process preserves original token in the queue
            generator.Process(this.GetToken(any));
            Assert.IsTrue(generator.HasTokens);

            // NextToken drains the queue
            Assert.AreEqual(any, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts indention tokens on whitespaces 
            generator.Process(this.GetToken(whitespace, " "));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(whitespace, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on whitespaces
            generator.Process(this.GetToken(whitespace, "    "));
            generator.Process(this.GetToken(whitespace, " "));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(whitespace, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(dedent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(whitespace, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on EOF
            generator.Process(this.GetToken(Constant.Eof));
            Assert.AreEqual(dedent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(Constant.Eof, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);
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

            this.generator.Process(leadingWhitespaceToken);
            var token = this.generator.NextToken();

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
            
            this.generator.Process(this.GetToken(whitespace, " "));
            this.generator.NextToken();
            this.generator.NextToken();
            this.generator.Process(eofToken);
            var token = this.generator.NextToken();

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
            
            this.generator.Process(this.GetToken(whitespace, new string(' ', tabLength - spaceLength)));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.AreEqual(whitespace, generator.NextToken().Type);

            this.generator.Process(this.GetToken(whitespace, "\t"));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.AreEqual(whitespace, generator.NextToken().Type);

            this.generator.Process(this.GetToken(whitespace, new string(' ', tabLength + spaceLength)));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.AreEqual(whitespace, generator.NextToken().Type);
        }

        private IToken GetToken(int type, string text = "")
        {
            return new CommonToken(type, text) { CharPositionInLine = 0 };
        }
    }
}

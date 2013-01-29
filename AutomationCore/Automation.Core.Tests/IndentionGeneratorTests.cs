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
        private IToken GetToken(int type, int length = 0)
        {
            return new CommonToken(type, new string(' ', length));
        }

        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            var any = 99;
            var indent = 5;
            var dedent = 10;
            var whitespace = 32;
            var generator = IndentionGenerator.GetInstance(indent, dedent, whitespace);

            var anyToken = this.GetToken(any);
            var whitespaceToken = new CommonToken(whitespace);
            var eofToken = new CommonToken(Constant.Eof);

            // HasTokens shows if there are tokens left in the queue
            Assert.IsFalse(generator.HasTokens);

            // Process puts any token in the queue
            generator.Process(anyToken);
            Assert.IsTrue(generator.HasTokens);

            // NextToken drains the queue
            var token = generator.NextToken();
            Assert.AreSame(anyToken, token);
            Assert.IsFalse(generator.HasTokens);

            // Process puts indention tokens on whitespaces 
            generator.Process(this.GetToken(whitespace, 1));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on whitespaces
            generator.Process(this.GetToken(whitespace, 2));
            generator.Process(this.GetToken(whitespace, 1));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(dedent, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on EOF
            generator.Process(eofToken);
            Assert.AreEqual(dedent, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);
        }

        [TestMethod]
        public void Generated_INDENT_token_is_correct()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Generated_DEDENT_token_is_correct()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Position_uses_1_for_spaces_and_4_for_tabs()
        {
            Assert.Inconclusive();
        }
    }
}

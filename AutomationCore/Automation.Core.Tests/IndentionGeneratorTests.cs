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
        private const int any = 99;
        private const int indent = 5;
        private const int dedent = 10;
        private const int whitespace = 32;

        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            var generator = IndentionGenerator.GetInstance(indent, dedent, whitespace);

            // HasTokens shows if there are tokens left in the queue
            Assert.IsFalse(generator.HasTokens);

            // Process preserves original token in the queue
            generator.Process(this.GetToken(any));
            Assert.IsTrue(generator.HasTokens);

            // NextToken drains the queue
            Assert.AreEqual(any, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts indention tokens on whitespaces 
            generator.Process(this.GetToken(whitespace, 1));
            Assert.AreEqual(indent, generator.NextToken().Type);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual(whitespace, generator.NextToken().Type);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on whitespaces
            generator.Process(this.GetToken(whitespace, 2));
            generator.Process(this.GetToken(whitespace, 1));
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

        private IToken GetToken(int type, int length = 0)
        {
            return new CommonToken(type, new string(' ', length)) { CharPositionInLine = 0 };
        }
    }
}

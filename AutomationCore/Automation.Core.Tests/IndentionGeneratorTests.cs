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
        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            var generator = IndentionGenerator.GetInstance();
            var anyToken = new CommonToken(42);
            var whitespace = new CommonToken(32);
            var eof = new CommonToken(-1);

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
            generator.Process(whitespace);
            token = generator.NextToken();
            Assert.AreEqual("INDENT", token.Text);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on whitespaces
            generator.Process(whitespace);
            generator.Process(whitespace);
            token = generator.NextToken();
            Assert.AreEqual("INDENT", token.Text);
            Assert.IsTrue(generator.HasTokens);
            Assert.AreEqual("DEDENT", token.Text);
            Assert.IsFalse(generator.HasTokens);

            // Process puts dedention tokens on EOF
            generator.Process(eof);
            token = generator.NextToken();
            Assert.AreEqual("DEDENT", token.Text);
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

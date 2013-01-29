using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class IndentionGeneratorTests
    {
        [TestMethod]
        public void Token_generation_workflow_works_correctly()
        {
            // HasTokens shows that there are tokens left in the queue
            // Process puts any token in the queue
            // Process puts indention tokens on whitespaces and EOF
            // NextToken drains the queue

            Assert.Inconclusive();
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

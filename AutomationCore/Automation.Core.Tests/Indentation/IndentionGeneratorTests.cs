using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests.Indentation
{
    [TestClass]
    public class IndentionGeneratorTests
    {
        [TestMethod]
        public void Indention_is_triggered_for_any_token_that_is_first_in_line()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Indention_is_triggered_for_EOF_token_disregarding_of_its_position()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Indention_is_generated_based_on_position_of_leading_whitespaces()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Indention_is_generated_for_EOF_as_if_it_first_in_line_disregarding_of_its_actual_position()
        {
            Assert.Inconclusive();
        }
    }
}

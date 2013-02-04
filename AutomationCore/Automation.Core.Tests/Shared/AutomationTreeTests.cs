using System.Linq;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests.Shared
{
    [TestClass]
    public class AutomationTreeTests
    {
        private const int anyType = 4;

        [TestMethod]
        public void Retrieves_children_by_name_case_insensitive()
        {
            var matchWithSubLower = Node("match", Node("sub node"));
            var matchNoSubUpper = Node("match");
            var root = Node("root", matchWithSubLower, Node("no match"), matchNoSubUpper);

            var found = root.Find("Match").ToArray();
            Assert.AreEqual(2, found.Count());
            Assert.AreSame(matchWithSubLower, found.First());
            Assert.AreSame(matchNoSubUpper, found.Last());
        }

        [TestMethod]
        public void Handles_no_children_without_exception()
        {
            var nodeWithoutChildren = Node("root");
            var noFindings = nodeWithoutChildren.Find("child");
            Assert.AreEqual(0, noFindings.Count());
        }

        private AutomationTree Node(string text, params ITree[] children)
        {
            var token = new CommonToken(anyType, text);
            var node = new AutomationTree(token);
            node.AddChildren(children);
            return node;
        }
    }
}

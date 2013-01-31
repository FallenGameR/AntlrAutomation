﻿using System.Linq;
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
        public void Retrieves_children_by_name_correctly()
        {
            var matchWithSub = Node("match", Node("sub node"));
            var matchNoSub = Node("match");
            var root = Node("root", matchWithSub, Node("no match"), matchNoSub);

            var found = root.Find("match").ToArray();
            Assert.AreEqual(2, found.Count());
            Assert.AreSame(matchWithSub, found.First());
            Assert.AreSame(matchNoSub, found.Last());
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

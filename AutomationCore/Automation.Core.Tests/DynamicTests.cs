using System.Collections.Generic;
using System.Linq;
using Antlr.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class DynamicTests
    {
        private const int anyType = 1;
        private const string anyText = "text";
        private const int anyPosition = 2;
        private const int anyLine = 3;
        private const int anyTokenIndex = 4;

        [TestMethod]
        public void Static_properties_can_be_accessed_with_case_insensitive_names()
        {
            var child = new AutomationTree(new CommonToken());
            var token = new CommonToken
            {
                Type = anyType,
                Text = anyText,
                CharPositionInLine = anyPosition,
                Line = anyLine,
                TokenIndex = anyTokenIndex,
            };
            var node = new AutomationTree(token);
            node.AddChild(child);
            dynamic tree = node;

            // Case sensitive
            Assert.AreEqual(anyType, tree.Type);
            Assert.AreEqual(anyPosition, tree.CharPositionInLine);
            Assert.AreEqual(1, tree.ChildCount);
            Assert.AreEqual(-1, tree.ChildIndex);
            Assert.AreEqual(anyLine, tree.Line);
            Assert.AreEqual(anyText, tree.Text);
            Assert.AreEqual(anyTokenIndex, tree.TokenStartIndex);
            Assert.AreEqual(anyTokenIndex, tree.TokenStopIndex);
            Assert.AreSame(child, tree.Children[0]);
            Assert.AreSame(token, tree.Token);
            Assert.IsFalse(tree.IsNil);
            Assert.IsNull(tree.Parent);

            // Case insensitive
            Assert.AreEqual(anyType, tree.type);
            Assert.AreSame(child, tree.CHILDREN[0]);
        }

        [TestMethod]
        public void Dynamic_properties_return_children_with_corresponding_case_insensitive_name()
        {
            var anySectionLower = new AutomationTree(new CommonToken(anyType, "section"));
            var anySectionUpper = new AutomationTree(new CommonToken(anyType, "SECTION"));
            var otherChild = new AutomationTree(new CommonToken(anyType, "other"));
            var root = new AutomationTree(new CommonToken(anyType, "root"));
            root.AddChild(anySectionLower);
            root.AddChild(otherChild);
            root.AddChild(anySectionUpper);

            dynamic node = root;
            IEnumerable<AutomationTree> found = node.Section;

            Assert.AreEqual(2, found.Count());
            Assert.AreSame(anySectionLower, found.First());
            Assert.AreSame(anySectionUpper, found.Last());
        }
    }
}

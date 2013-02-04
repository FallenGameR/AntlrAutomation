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
        public void Static_properties_can_be_accessed_with_case_sensitive_names()
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
        }

        [TestMethod]
        public void Dynamic_properties_return_children_with_corresponding_case_insensitive_name()
        {
            Assert.Inconclusive();
        }
    }
}

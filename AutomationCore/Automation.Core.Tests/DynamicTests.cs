using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
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
        private const int anyStartIndex = 5;
        private const int anyStopIndex = 6;

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
                StartIndex = anyStartIndex,
                StopIndex = anyStopIndex,
            };
            var node = new AutomationTree(token);
            node.AddChild(child);

            dynamic tree = node;
            Assert.AreEqual(anyType, tree.Type);
            Assert.AreEqual(anyType, tree.type);

            //tree.CharPositionInLine
            //tree.ChildCount
            //tree.ChildIndex
            //tree.Children
            //tree.IsNil
            //tree.Line
            //tree.Parent
            //tree.Text
            //tree.Token
            //tree.TokenStartIndex
            //tree.TokenStopIndex
            //

            Assert.Inconclusive();
        }

        [TestMethod]
        public void Dynamic_properties_return_children_with_corresponding_case_insensitive_name()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [Ignore]
        public void DynamicTest()
        {
            dynamic obj = new AutomationTree();
            var test = obj.Test;
            Assert.AreEqual("text", obj.SomeText);
        }
    }
}

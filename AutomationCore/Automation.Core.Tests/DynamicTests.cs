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
        [TestMethod]
        public void Static_properties_can_be_accessed_with_case_insensitive_names()
        {
            var node = new AutomationTree(new CommonToken());
            //node.CharPositionInLine
            //node.ChildCount
            //node.ChildIndex
            //node.Children
            //node.IsNil
            //node.Line
            //node.Parent
            //node.Text
            //node.Token
            //node.TokenStartIndex
            //node.TokenStopIndex
            //node.Type

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

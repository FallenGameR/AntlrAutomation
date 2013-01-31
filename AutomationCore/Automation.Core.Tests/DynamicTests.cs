using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class DynamicTests
    {
        [TestMethod]
        public void Static_properties_can_be_accessed_with_case_insensitive_names()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Dynamic_properties_return_children_with_corresponding_name()
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

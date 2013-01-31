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
        public void DynamicTest()
        {
            dynamic obj = (dynamic)new AutomationTree();
            Assert.AreEqual("text", obj.SomeText);
        }
    }
}

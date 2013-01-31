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
        // Static tree properties can be accessed

        // Dynamic properties return children with corresponding name

        [TestMethod]
        public void DynamicTest()
        {
            dynamic obj = new AutomationTree();
            var test = obj.Test;
            Assert.AreEqual("text", obj.SomeText);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class IndentionTests
    {
        [TestMethod]
        public void Indentions_are_defined()
        {
            Assert.IsNotNull(Indention.Indent);
            Assert.IsNotNull(Indention.Dedent);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class ConstantTests
    {
        [TestMethod]
        public void Eof_is_defined_as_in_Antlr()
        {
            Assert.AreEqual(-1, Constant.Eof);
        }
    }
}

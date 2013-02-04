using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    [TestClass]
    public class PowershellTests
    {
        [TestMethod]
        public void Powershell_stdout_is_returned_sucessfully()
        {
            var actual = Powershell.Script("'hello world'");
            var expected = "hello world";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, Powershell.Out);
            Assert.IsTrue(string.IsNullOrEmpty(Powershell.Err));
        }

        [TestMethod]
        public void Powershell_stderr_is_returned_sucessfully()
        {
            // net use xxx
            var actual = Powershell.Script("[Console]::Error.WriteLine('test')");
            var expected = "test";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, Powershell.Err);
            Assert.IsTrue(string.IsNullOrEmpty(Powershell.Out));
        }

        [TestMethod]
        public void Powershell_write_error_is_returned_sucessfully()
        {
            var actual = Powershell.Script("Write-Error 'message'");
            var expected = "message";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, Powershell.Err);
        }

        [TestMethod]
        public void Powershell_throw_is_returned_sucessfully()
        {
            var actual = Powershell.Script("throw 'info'");
            var expected = "info";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, Powershell.Out);
            Assert.IsTrue(string.IsNullOrEmpty(Powershell.Out));
        }
    }
}

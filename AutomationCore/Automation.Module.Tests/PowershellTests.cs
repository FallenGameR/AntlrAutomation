using System;
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
            var actual = Powershell.Script("[Console]::Error.WriteLine('test')");
            var expected = "test";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, Powershell.Err);
            Assert.IsTrue(string.IsNullOrEmpty(Powershell.Out));
        }

        [TestMethod]
        public void Powershell_mixed_output_is_returned_sucessfully()
        {
            var actual = Powershell.Script(@"
'stdout'
[Console]::Error.WriteLine('stderr')
");
            var expected = "stdout" + Environment.NewLine + "stderr";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("stdout", Powershell.Out);
            Assert.AreEqual("stderr", Powershell.Err);
        }

        [TestMethod]
        public void Powershell_playground()
        {
            var test = Powershell.Script(@"
Import-Module ..\..\..\..\Module\AntlrAutomation.psd1
");
            


            Assert.Inconclusive();
        }
    }
}

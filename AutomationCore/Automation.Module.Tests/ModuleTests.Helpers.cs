using System;
using System.Linq;
using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    public partial class ModuleTests
    {
        private void UseAst(string testScript)
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleShort.g3'
$ast = Parse-Item sample 'Temp\Sample.txt';
"
+
testScript
);
        }

        private void TestAst(string testScript)
        {
            var lineCount = testScript
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Count();

            this.UseAst(testScript);

            var expected = string.Join(
                Environment.NewLine,
                Enumerable.Repeat("True", lineCount).ToArray());

            Assert.AreEqual(expected, Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        /*
         * NOTE: Looks like that is a bug in Powershell 3.0
         * 
$a = New-Object System.Dynamic.ExpandoObject
$a.One = "1"

# long syntax works as expected
» $a,$a | foreach{ $psitem.One }
1
1

# short syntax does not output anything
» $a,$a | foreach One
         * 
         * 
        [TestMethod]
        public void Dynamic_children_properties_can_be_retrieved_via_foreach_syntax_case_insensitive()
        {
            this.TestAst(
@"
@($ast | % SECTION | % here).count -eq 2
@($ast | % Section | % HeRe).count -eq 2
");
        }
        /**/
    }
}

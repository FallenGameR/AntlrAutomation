using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void Module_can_be_imported_without_errors()
        {
            Powershell.Script(
@"
Import-Module ..\..\..\..\Module\AntlrAutomation.psd1
");
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Set_Grammar_accepts_full_text_grammar_without_errors()
        {
            Powershell.Script(
@"
# Removing old compiled grammars for clear test run
del '$PSScriptRoot\Parsers\Simpleton' -Recurse *> $null

# Simpleton grammar compiles without errors
$fullText = type '$PSScriptRoot\..\Info\simpleton.g3' | Out-String
Set-Grammar -Text $fullText
");

            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        /*
        [TestMethod]
        public void Set_Grammar_accepts_short_text_grammar_without_errors()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Parse_Item_parses_input_with_case_insensitive_grammar_name()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Parse_Item_parses_input_with_regex_matches_grammar_name()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Parse_Item_chooses_first_matching_grammar_and_print_warning_if_grammar_name_ambiguity()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Format_custom_is_used_by_default()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Format_custom_renders_output_as_tree()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Format_table_renders_output_as_table()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Dynamic_children_access_works()
        {
            Assert.Inconclusive();
        }
        /**/
    }
}

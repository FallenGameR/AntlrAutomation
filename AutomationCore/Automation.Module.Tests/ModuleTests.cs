using System;
using System.IO;
using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestInitialize]
        public void Initialize()
        {
            // Current folder is Module root
            Environment.CurrentDirectory = Environment.CurrentDirectory + @"\..\..\..\..\Module\";

            // Delete Temp folder is exists
            if (Directory.Exists("Temp"))
            {
                Directory.Delete("Temp", recursive: true);
            }

            // Create new temp folder
            Directory.CreateDirectory("Temp");

            // Delete all sample parsers
            foreach (var parserFolder in Directory.GetDirectories("Parsers", "Sample*"))
            {
                Directory.Delete(parserFolder, recursive: true);
            }

            // Create temp files used in tests
            File.WriteAllText("Temp/SampleFull.g3", Resources.SampleFull);
        }

        [TestMethod]
        public void Module_can_be_imported_without_errors()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
");
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Set_Grammar_accepts_full_text_grammar_without_errors()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleFull.g3' -Verbose
");

            /*
out: VERBOSE: Parser folder 'C:\src\github\AntlrAutomation\Module\Parsers\SampleFull' is cleaned for parser 'SampleFull'
out: VERBOSE: Parser grammar text set for parser 'SampleFull'
out: VERBOSE: Parser sources generated for parser 'SampleFull'
out: VERBOSE: Parser binaries compiled for parser 'SampleFull'
            /**/

            Assert.AreEqual(string.Empty, Powershell.Out);
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

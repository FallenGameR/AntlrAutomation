using System;
using System.IO;
using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    [TestClass]
    public class ModuleTests
    {
        private string oldCurrentDirectory;

        [TestInitialize]
        public void Initialize()
        {
            oldCurrentDirectory = Environment.CurrentDirectory;

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
            File.WriteAllText("Temp/SampleShort.g3", Resources.SampleShort);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete all sample parsers
            foreach (var parserFolder in Directory.GetDirectories("Parsers", "Sample*"))
            {
                Directory.Delete(parserFolder, recursive: true);
            }

            // Delete Temp folder is exists
            if (Directory.Exists("Temp"))
            {
                Directory.Delete("Temp", recursive: true);
            }

            // Restore original current folder
            Environment.CurrentDirectory = oldCurrentDirectory;
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
        public void Set_Grammar_accepts_full_text_grammar_successfully()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleFull.g3' -Verbose
");

            var parserFolder = Path.Combine(Environment.CurrentDirectory, @"Parsers\SampleFull");
            var expected =
@"
VERBOSE: Folder '<PARSER FOLDER>' is cleaned for parser 'SampleFull'
VERBOSE: Grammar file set for parser 'SampleFull'
VERBOSE: Sources generated for parser 'SampleFull'
VERBOSE: Binaries compiled for parser 'SampleFull'
"
                .Replace("<PARSER FOLDER>", parserFolder)
                .Trim();

            Assert.AreEqual(expected, Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Set_Grammar_accepts_short_text_grammar_successfully()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleShort.g3' -Verbose
");

            var parserFolder = Path.Combine(Environment.CurrentDirectory, @"Parsers\SampleShort");
            var expected =
@"
VERBOSE: Folder '<PARSER FOLDER>' is cleaned for parser 'SampleShort'
VERBOSE: Grammar file set for parser 'SampleShort'
VERBOSE: Sources generated for parser 'SampleShort'
VERBOSE: Binaries compiled for parser 'SampleShort'
"
                .Replace("<PARSER FOLDER>", parserFolder)
                .Trim();

            Assert.AreEqual(expected, Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        /*
        [TestMethod]
        public void Parse_Item_parses_input_with_case_insensitive_grammar_name()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Parse_Item_parses_input_with_regex_matched_grammar_name()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Parse_Item_chooses_first_matching_grammar_and_prints_warning_about_grammar_name_ambiguity()
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

﻿using System;
using System.IO;
using Automation.Module.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    /// <summary>
    /// Tests for the AntlrAutomation module
    /// </summary>
    /// <remarks>
    /// Tests check console output. Powershell tries to be smart about console width
    /// and thus your console window settings could affect the test outcome.
    /// </remarks>
    [TestClass]
    public partial class ModuleTests
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
            File.WriteAllText("Temp/ImaginaryGrammar.g3", Resources.ImaginaryGrammar);
            File.WriteAllText("Temp/Sample.txt", Resources.SampleText);
            File.WriteAllText("Temp/Imaginary.txt", Resources.ImaginaryText);
        }

        [TestCleanup]
        public void Cleanup()
        {
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
.Replace("<PARSER FOLDER>", parserFolder);

            Assert.AreEqual(expected.Trim(), Powershell.Out, ignoreCase: true);
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
.Replace("<PARSER FOLDER>", parserFolder);

            Assert.AreEqual(expected.Trim(), Powershell.Out, ignoreCase: true);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Parse_Item_parses_input_with_case_insensitive_grammar_name()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleShort.g3'
Parse-Item sampleSHORT 'Temp\Sample.txt' | % Text
");
            Assert.AreEqual("FILE", Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Parse_Item_parses_input_with_regex_matched_grammar_name()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleShort.g3'
Parse-Item short 'Temp\Sample.txt' | % Text
");
            Assert.AreEqual("FILE", Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Parse_Item_chooses_first_matching_grammar_and_prints_warning_about_grammar_name_ambiguity()
        {
            Powershell.Script(
@"
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\SampleFull.g3'
Set-Grammar 'Temp\SampleShort.g3'
Parse-Item sample 'Temp\Sample.txt' | % Text
");
            var expected =
@"
WARNING: Grammar 'sample' can be resolved as: SampleFull, SampleShort. Grammar 'SampleFull' would be used.
FILE
";
            Assert.AreEqual(expected.Trim(), Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Format_custom_renders_output_as_tree()
        {
            this.UseAst("$ast | Format-Custom");
            var expected =
@"
  FILE
    SECTION
      some
      words
      here
    SECTION
      another
      section
      here
";
            Assert.AreEqual(expected.Trim(), Powershell.Out.Trim());
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Format_table_renders_output_as_table()
        {
            this.UseAst("$ast | Format-Table");
            var expected =
@"
Text Children          
---- --------          
FILE {SECTION, SECTION}
";
            Assert.AreEqual(expected.Trim(), Powershell.Out.Trim());
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Format_custom_is_used_by_default()
        {
            this.TestAst("($ast | Out-String) -eq ($ast | fc | Out-String)");
        }

        [TestMethod]
        public void Static_properties_continue_to_work_with_case_insensitive_match()
        {
            this.TestAst(
@"
$ast.Text -eq 'FILE'
$ast.text -eq 'FILE'
$ast.TEXT -eq 'FILE'
$ast.TeXt -eq 'FILE'
");
        }

        [TestMethod]
        public void Dynamic_children_properties_can_be_retrieved_with_case_insensitive_match()
        {
            this.TestAst(
@"
@($ast.SECTION.some).count -eq 1
@($ast.Section.HeRe).count -eq 2
");
        }

        [TestMethod]
        public void Imaginary_tokens_are_automatically_detected()
        {
            Powershell.Script(
@"  
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\ImaginaryGrammar.g3'
Parse-Item imaginary 'Temp\Imaginary.txt' | % Text
");
            Assert.AreEqual("ANY_TOKEN", Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void Tokenization_works_fine()
        {
            Powershell.Script(
@"  
Import-Module .\AntlrAutomation.psd1
Set-Grammar 'Temp\ImaginaryGrammar.g3'

$old = [Console]::Out
$writer = New-Object Io.StringWriter
[Console]::SetOut( $writer )

Parse-Item imaginary 'Temp\Imaginary.txt' -Tokens

$writer.Flush()
[Console]::SetOut( $old )
$writer.GetStringBuilder().ToString()
");
            Assert.AreEqual("VARIABLE - ID <NL>" + Environment.NewLine + "<EOF>" + Environment.NewLine, Powershell.Out);
            Assert.AreEqual(string.Empty, Powershell.Err);
        }

        [TestMethod]
        public void By_default_no_indents_newlines_and_whitespaces_are_generated()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Indents_can_be_emitted()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Whitespaces_can_be_emitted()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Newlines_can_be_emitted()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Indents_and_dedents_are_emitted_correctly()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void No_caching_effect_after_using_the_same_string_templates()
        {
            Assert.Inconclusive();
        }
    }
}

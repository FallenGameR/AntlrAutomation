using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests.Indentation
{
    [TestClass]
    public class IndentionGeneratorTests
    {
        private const int any = 10;
        private const int indent = 20;
        private const int dedent = 30;
        private const int whitespace = 40;
        private const int channel = 50;

        private const int firstInLine = 0;
        private const int notFirstInLine = 1;

        private const int anyPosition = 1;
        private const int anyLine = 2;
        private const int anyIndex = 3;
        private const int anyChannel = 4;

        private IGenerator generator;

        [TestInitialize]
        public void Initialize()
        {
            this.generator = IndentionGenerator.GetInstance(indent, dedent, whitespace, channel);
        }

        [TestMethod]
        public void Indention_is_triggered_for_any_token_that_is_first_in_line()
        {
            var firstTokenInLine = this.GetToken(any, firstInLine);
            Assert.IsTrue(this.generator.IsTrigger(firstTokenInLine));

            var notFirstTokenInLine = this.GetToken(any, notFirstInLine);
            Assert.IsFalse(this.generator.IsTrigger(notFirstTokenInLine));
        }

        [TestMethod]
        public void Indention_is_triggered_for_EOF_token_disregarding_of_its_position()
        {
            var eofFirstInLine = this.GetToken(Constant.Eof, firstInLine);
            Assert.IsTrue(this.generator.IsTrigger(eofFirstInLine));

            var eofNotFirstInLine = this.GetToken(Constant.Eof, notFirstInLine);
            Assert.IsTrue(this.generator.IsTrigger(eofNotFirstInLine));
        }

        [TestMethod]
        public void Indention_is_generated_based_on_position_of_leading_whitespaces()
        {
            var noLeadingWhitespaces = this.GetToken(any, "string");
            Assert.AreEqual(0, this.generator.Generate(noLeadingWhitespaces).Count());

            var threeLeadingSpaces = this.GetToken(any, "   this is one space less then a tab");
            Assert.AreEqual(indent, this.generator.Generate(threeLeadingSpaces).Single().Type);

            var leadingTab = this.GetToken(any, "\t. Tab is like four spaces");
            Assert.AreEqual(indent, this.generator.Generate(leadingTab).Single().Type);
        }

        [TestMethod]
        public void Indention_is_generated_for_EOF_as_if_it_first_in_line_disregarding_of_its_actual_position()
        {
            var anyTokenThatGeneratesIndent = this.GetToken(any, " ");
            Assert.AreEqual(indent, this.generator.Generate(anyTokenThatGeneratesIndent).Single().Type);

            var eofThatIsNotFirstInLin = this.GetToken(Constant.Eof, notFirstInLine);
            Assert.AreEqual(dedent, this.generator.Generate(eofThatIsNotFirstInLin).Single().Type);
        }

        [TestMethod]
        public void Indention_generator_uses_correct_channel_and_text_for_generated_tokens()
        {
            var indentEmitter = this.GetToken(any, " ");
            var indentToken = this.generator.Generate(indentEmitter).Single();

            Assert.AreEqual(indent, indentToken.Type);
            Assert.AreEqual("INDENT", indentToken.Text);
            Assert.AreEqual(channel, indentToken.Channel);

            var dedentEmitter = this.GetToken(any, string.Empty);
            var dedentToken = this.generator.Generate(dedentEmitter).Single();

            Assert.AreEqual(dedent, dedentToken.Type);
            Assert.AreEqual("DEDENT", dedentToken.Text);
            Assert.AreEqual(channel, dedentToken.Channel);
        }

        private IToken GetToken(int type, int position)
        {
            return new CommonToken(type) { CharPositionInLine = position };
        }

        private IToken GetToken(int type, string text)
        {
            return new CommonToken(type, text) { CharPositionInLine = firstInLine };
        }
    }
}

using System.Linq;
using Antlr.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class BeginningOfLineGeneratorTests
    {
        private const int any = 42;
        private const int bol = 53;
        private const int first = 0;
        private const int notFirst = 1;
        private const int someLine = 4;

        private IGenerator generator;

        [TestInitialize]
        public void Initialize()
        {
            this.generator = BeginningOfLineGenerator.GetInstance(bol);
        }

        [TestMethod]
        public void Any_not_EOF_first_in_line_toke_is_the_trigger()
        {
            Assert.IsTrue(this.generator.IsTrigger(this.GetToken(any, first)));
            Assert.IsFalse(this.generator.IsTrigger(this.GetToken(any, notFirst)));
            Assert.IsFalse(this.generator.IsTrigger(this.GetToken(Constant.Eof, first)));
            Assert.IsFalse(this.generator.IsTrigger(this.GetToken(Constant.Eof, notFirst)));
        }

        [TestMethod]
        public void Generates_beginning_of_line_token_with_correct_properties()
        {
            var bolToken = this.generator.Generate(this.GetToken(any, line: someLine)).Single();

            Assert.AreEqual(bol, bolToken.Type);
            Assert.AreEqual(Lexer.DefaultTokenChannel, bolToken.Channel);
            Assert.AreEqual(string.Empty, bolToken.Text);
            Assert.AreEqual(0, bolToken.CharPositionInLine);
            Assert.AreEqual(someLine, bolToken.Line);
        }

        private IToken GetToken(int type, int position = any, int line = any)
        {
            return new CommonToken(type)
            {
                CharPositionInLine = position,
                Line = line,
            };
        }
    }
}

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ind = Automation.Core.Indention;

namespace Automation.Core.Tests
{
    [TestClass]
    public class IndentionDetectorTests
    {
        [TestMethod]
        public void Indention_detected_correctly_on_well_formatted_cases()
        {
/*
0
  1
0
*/
            this.VerifyIndentions(
                new[] { 0, 1, 0 }, 
                Ind.Indent, Ind.Dedent);
/*
 1
*/
            this.VerifyIndentions(
                new[] { 1 },
                Ind.Indent);
/*
 1
0
*/
            this.VerifyIndentions(
                new[] { 1, 0 },
                Ind.Indent, Ind.Dedent);
            
/*
0
 1
  2
0
*/
            this.VerifyIndentions(
                new[] { 0, 1, 2, 0 }, 
                Ind.Indent, Ind.Indent, Ind.Dedent, Ind.Dedent);
/*
0
  2
  2
    4
    4
0
*/
            this.VerifyIndentions(
                new[] { 0, 2, 2, 4, 4, 0 },
                Ind.Indent, Ind.Indent, Ind.Dedent, Ind.Dedent);/*
0
 1
   3
 1
 1
  2
  2
  2
 1
0
*/
            this.VerifyIndentions(
                new[] { 0, 1, 3, 1, 1, 2, 2, 2, 1, 0 },
                Ind.Indent, Ind.Indent, Ind.Dedent,  // 0, 1, 3, 1, 1,
                Ind.Indent, Ind.Dedent, Ind.Dedent); // 2, 2, 2, 1, 0
        }

        [TestMethod]
        public void Indention_dedection_fails_with_AutomationException_on_not_well_formatted_cases()
        {
/*
0
  2
 1
*/
            // It is impossible to put Dedent since there is no matching indent block for it
            try
            {
                this.VerifyIndentions(
                    new[] { 0, 2, 1 },
                    Ind.Indent, Ind.Dedent);
                Assert.Fail("This input is not well formatted");
            }
            catch (AutomationException ex)
            {
                Assert.AreEqual(
                    "Input is not well formatted, can't produce correct indention",
                    ex.Message);
            }
        }

        private void VerifyIndentions(int[] positions, params Ind[] expectedIndentions)
        {
            var detector = IndentionDetector.GetInstance();

            var actualIndentions =
                from position in positions
                from indention in detector.Detect(position)
                select indention;

            CollectionAssert.AreEqual(expectedIndentions, actualIndentions.ToArray());
        }
    }
}

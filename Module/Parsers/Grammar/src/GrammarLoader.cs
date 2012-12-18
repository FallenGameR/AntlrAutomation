using System;
using Antlr.Runtime;

namespace Automation.Core.Grammar
{
    public class GrammarLoader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new GrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new GrammarParser(tokens) { TreeAdaptor = new AutomationAdaptor() };
            return parser.file().Tree;
        }
    }
}

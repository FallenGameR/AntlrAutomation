using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Automation.Core;
using Automation.Core.Grammar;

namespace Sample.Parser
{
    public class Loader : MarshalByRefObject, ILoader
    {
        public CommonTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new GrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new GrammarParser(tokens) { TreeAdaptor = new AutomationAdaptor() };
            return parser.file().Tree;
        }
    }
}

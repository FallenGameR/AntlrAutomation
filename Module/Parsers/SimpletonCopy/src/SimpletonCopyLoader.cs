using System;
using Antlr.Runtime;
using Automation.Core;

namespace Automation.Parsers.SimpletonCopyGrammar
{
    public class SimpletonCopyLoader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new SimpletonCopyLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new SimpletonCopyParser(tokens) { TreeAdaptor = new AutomationAdaptor() };
            return parser.file().Tree;
        }
    }
}

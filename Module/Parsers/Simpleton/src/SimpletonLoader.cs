using System;
using Antlr.Runtime;
using Automation.Core;

namespace Automation.Parsers.Simpleton
{
    public class SimpletonLoader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new SimpletonLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new SimpletonParser(tokens) { TreeAdaptor = new AutomationAdaptor() };
            return parser.file().Tree;
        }
    }
}

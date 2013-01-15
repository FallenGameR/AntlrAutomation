using System;
using Antlr.Runtime;
using Automation.Core;

namespace Automation.Parsers.ShortGrammar
{
    public class ShortLoader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new ShortLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new ShortParser(tokens) 
            { 
                TreeAdaptor = new AutomationAdaptor(),
            };
            return parser.file().Tree;
        }
    }
}

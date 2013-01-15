using System;
using Antlr.Runtime;
using Automation.Core;

namespace Automation.Parsers.shortGrammar
{
    public class shortLoader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new shortLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new shortParser(tokens) 
            { 
                TreeAdaptor = new AutomationAdaptor(),
            };
            return parser.file().Tree;
        }
    }
}

using Antlr.Runtime;
using Antlr.Runtime.Tree;
using System;

namespace ParserLibrary
{
    public class Loader: MarshalByRefObject
    {
        public CommonTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new GrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new GrammarParser(tokens);
            return parser.file().Tree;
        }
    }
}

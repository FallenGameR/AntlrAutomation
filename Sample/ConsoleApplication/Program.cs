using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var file = @"d:\Archive\Projects\AntlrAutomation\Sample\Resources\simpleton.txt";
            var stream = new ANTLRFileStream(file);
            var lexer = new SimpletonLexer();
            var tokens = new CommonTokenStream(lexer);
            var parser = new SimpletonParser(tokens);
            var tree = parser.file().Tree;
        }
    }
}

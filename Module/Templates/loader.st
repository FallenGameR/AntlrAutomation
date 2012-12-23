﻿loader (name) ::=
<<
using System;
using Antlr.Runtime;

namespace Automation.Core.<name>
{
    public class <name>Loader : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new <name>Lexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new <name>Parser(tokens) { TreeAdaptor = new AutomationAdaptor() };
            return parser.file().Tree;
        }
    }
}
>>
loader (name) ::=
<<
using System;
using System.Collections.Generic;
using Antlr.Runtime;
using Automation.Core;

namespace <namespace(name=name)>
{
    public class <loaderName(name=name)> : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new <lexerName(name=name)>(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new <parserName(name=name)>(tokens) 
            { 
                TreeAdaptor = new AutomationAdaptor(),
            };
            return parser.file().Tree;
        }

        public IEnumerable\<string\> Tokenize(string filePath)
        {
            var stream = new ANTLRFileStream(filePath);
            var lexer = new <lexerName(name=name)>(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new <parserName(name=name)>(tokens);

            tokens.Fill();
        
            foreach( var token in tokens.GetTokens() )
            {
                yield return parser.RenderToken(token);
            }
        }
    }
}
>>

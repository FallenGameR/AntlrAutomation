loader (name) ::=
<<
using System;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Automation.Core;

namespace <namespace(name=name)>
{
    // NOTE: Dispose is not needed for the StreamReaders since we are dealing with MemoryStream

    public class <loaderName(name=name)> : MarshalByRefObject, ILoader
    {
        public AutomationTree Parse(string text)
        {
            var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)));
            var stream = new ANTLRReaderStream(reader);
            var lexer = new <lexerName(name=name)>(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new <parserName(name=name)>(tokens) 
            { 
                TreeAdaptor = new AutomationAdaptor(),
            };
            return parser.file().Tree;
        }

        public string[] Tokenize(string text)
        {
            var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)));
            var stream = new ANTLRReaderStream(reader);
            var lexer = new <lexerName(name=name)>(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new <parserName(name=name)>(tokens);

            tokens.Fill();
            return tokens
                .GetTokens()
                .Select( t =\> parser.RenderToken(t) )
                .ToArray();
        }
    }
}
>>

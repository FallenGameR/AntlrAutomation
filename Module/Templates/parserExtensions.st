parserExtensions (name) ::=
<<
using System;
using Antlr.Runtime;

namespace <namespace(name=name)>
{
    partial class <parserName(name=name)>
    {
        public string RenderToken( IToken token )
        {
            switch (token.Type)
            {
                case <parserName(name=name)>.WS:
                    return new string('-', token.Text.Length) + " ";

                case <parserName(name=name)>.NEWLINE:
                    return "\<NL\>" + Environment.NewLine;

                case <parserName(name=name)>.EOF:
                    return "\<EOF\>" + Environment.NewLine;

                default:
                    return this.TokenNames[token.Type].Trim('\'') + " ";
            }
        }
    }
}
>>

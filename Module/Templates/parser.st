parser (name) ::=
<<
using System;
using Antlr.Runtime;

namespace <names/namespace(name=name)>
{
    partial class <names/parser(name=name)>
    {
        public string RenderToken( IToken token )
        {
            switch (token.Type)
            {
                case <names/parser(name=name)>.WS:
                    return new string('-', token.Text.Length) + " ";

                case <names/parser(name=name)>.NEWLINE:
                    return "\<NL\>" + Environment.NewLine;

                case <names/parser(name=name)>.EOF:
                    return "\<EOF\>" + Environment.NewLine;

                default:
                    return this.TokenNames[token.Type].Trim('\'') + " ";
            }
        }
    }
}
>>

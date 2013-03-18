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
            var text = RenderTokenText(token);
            var isTerminator = text.StartsWith( "\<" );
        
            // Put hidden tokens in square braces
            if( token.Channel == <names/lexer(name=name)>.Hidden )
            {
                text = "[" + text + "]";
            }

            // Put new lines after terminator tokens
            if( isTerminator )
            {
                text += Environment.NewLine;
            }
            else
            {
                text += " ";
            }

            return text;
        }

        public string RenderTokenText( IToken token )
        {
            switch (token.Type)
            {
                case <names/parser(name=name)>.WS:
                    return new string('-', token.Text.Length);

                case <names/parser(name=name)>.BOL:
                    return "{" + token.Line.ToString() + "}";

                case <names/parser(name=name)>.EOL:
                    return "\<EOL\>";

                case <names/parser(name=name)>.INDENT:
                    return "\<INDENT\>";

                case <names/parser(name=name)>.DEDENT:
                    return "\<DEDENT\>";

                case <names/parser(name=name)>.EOF:
                    return "\<EOF\>";

                default:
                    return this.TokenNames[token.Type].Trim('\'');
            }
        }
    }
}
>>

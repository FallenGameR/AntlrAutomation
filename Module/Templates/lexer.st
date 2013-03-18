lexer (name, emitIndents, emitBeginningOfLine) ::=
<<
using Automation.Core;
using Antlr.Runtime;

namespace <names/namespace(name=name)>
{
    partial class <names/lexer(name=name)>
    {
        public const int HIDDEN = Hidden;

        private readonly Emitter emitter = 
            Emitter.GetInstance(
                IndentionGenerator.GetInstance(
                    <names/parser(name=name)>.INDENT, 
                    <names/parser(name=name)>.DEDENT, 
                    Lexer.<if(emitIndents)>DefaultTokenChannel<else>Hidden)<endif>),
                BeginningOfLineGenerator.GetInstance(
                    <names/parser(name=name)>.BOL,
                     Lexer.<if(emitBeginningOfLine)>DefaultTokenChannel<else>Hidden<endif>)
            );

        public override IToken NextToken()
        {
            if (this.emitter.HasTokens)
            {
                return this.emitter.NextToken();
            }

            var token = base.NextToken();
            this.emitter.Process(token);
            return this.emitter.NextToken();
        }
    }
}
>>

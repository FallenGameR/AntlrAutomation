﻿lexer (name) ::=
<<
using Automation.Core;
using Antlr.Runtime;

namespace <names/namespace(name=name)>
{
    partial class <names/lexer(name=name)>
    {
        public const int HIDDEN = Hidden;

        private readonly IndentionGenerator generator = 
            IndentionGenerator.GetInstance(
                <names/parser(name=name)>.INDENT, 
                <names/parser(name=name)>.DEDENT, 
                <names/parser(name=name)>.WS,
                <names/lexer(name=name)>.Hidden);

        public override IToken NextToken()
        {
            if (this.generator.HasTokens)
            {
                return this.generator.NextToken();
            }

            var token = base.NextToken();
            this.generator.Process(token);
            return this.generator.NextToken();
        }
    }
}
>>
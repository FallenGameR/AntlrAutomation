lexerExtensions (name) ::=
<<
using Automation.Core;
using Antlr.Runtime;

namespace <namespace(name=name)>
{
    partial class <lexerName(name=name)>
    {
        public const int HIDDEN = Hidden;

        private readonly IndentionGenerator generator = 
            IndentionGenerator.GetInstance(
                <parserName(name=name)>.INDENT, 
                <parserName(name=name)>.DEDENT, 
                <parserName(name=name)>.WS,
                <lexerName(name=name)>.DefaultTokenChannel);

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

lexerExtensions (name) ::=
<<
using Automation.Core;

namespace <namespace(name=name)>
{
    partial class <lexerName(name=name)>
    {
        public const int HIDDEN = Hidden;

        private readonly IndentionGenerator generator = 
            IndentionGenerator.GetInstance(
                <parserName(name=name)>.INDENT, 
                <parserName(name=name)>.DEDENT, 
                <parserName(name=name)>.WS);
    }
}
>>

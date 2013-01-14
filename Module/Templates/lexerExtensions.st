lexerExtensions (name) ::=
<<
namespace Automation.Parsers.<name>Grammar
{
    partial class <name>Lexer
    {
        public const int HIDDEN = Hidden;
    }
}
>>

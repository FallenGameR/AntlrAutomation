namespace Automation.Module.Tests.TestUtils
{
    public static class Resources
    {
        public static string SampleFull =
@"
grammar SampleFull;

options 
{
    language=CSharp3;
    output=AST;
    TokenLabelType=CommonToken;
    ASTLabelType=AutomationTree;
}

tokens {FILE; SECTION;}

@lexer::header{using Automation.Core;}
@parser::header{using Automation.Core;}
@lexer::namespace{Automation.Parsers.SampleFullGrammar}
@parser::namespace{Automation.Parsers.SampleFullGrammar}

/*
 * Parser Rules
 */

public file
    : section+ EOF -> ^(FILE section+)
    ;

section
    : 'start' WORD* 'end' -> ^(SECTION WORD*)
    ;

/*
 * Lexer Rules
 */

WORD: 'a'..'z'+;

NEWLINE
    : '\r'? '\n' { $channel = Hidden; };

WS	: (' ' | '\t')+ { $channel = Hidden; };
";

        public static string SampleShort =
@"
// Simple parser grammar

file: section+ EOF -> ^(FILE section+);

section
    : 'start' WORD* 'end' -> ^(SECTION WORD*);

WORD: 'a'..'z'+;
";

        public static string SampleText =
@"
start
    some
    words
    here
end

start
    another
    section
end
";
    }
}

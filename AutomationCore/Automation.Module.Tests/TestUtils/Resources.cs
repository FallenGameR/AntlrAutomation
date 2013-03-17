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

WS	: (' ' | '\t')+ { $channel = Hidden; };

EOL
    : '\r'? '\n' { $channel = Hidden; };

INDENT
    : '<%$! INDENT tokens are inserted via IndentionGenerator during lexing !$%>';

DEDENT
    : '<%$! DEDENT tokens are inserted via IndentionGenerator during lexing !$%>';
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
    here
end
";

        public static string ImaginaryGrammar =
@"
file: VARIABLE ID EOF -> ^(ANY_TOKEN VARIABLE ID);
VARIABLE
    : 'a'..'z'+;
ID  : '0'..'9'+;
";

        public static string ImaginaryText = "name 42";

        public static string IndentGrammar = @"
file: node+ EOF -> ^(ROOT node+);
node: ID (INDENT node+ DEDENT)? -> ^(ID node*);
ID  : ('a'..'z' | '_')+;
";

        public static string IndentText = @"
root_a
    child_b
    child_c
    subroot_d
        child_e
        child_f
    child_g
";

        public static string MultiTokenBaseGrammar = @"
file: ID ID EOF -> ^(BASE_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string MultiTokenNewlineGrammar = @"
file: ID EOL ID EOF -> ^(NEWLINE_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string MultiTokenWhitespaceGrammar = @"
file: ID WS WS ID EOF -> ^(WHITESPACE_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string MultiTokenIndentGrammar = @"
file:
    ID
    INDENT
        ID
    DEDENT
    EOF
    -> ^(INDENT_ROOT ID+);

ID  : 'a'..'z'+;
";

        public static string MultiTokenText = 
@"line
  indent";
    }
}

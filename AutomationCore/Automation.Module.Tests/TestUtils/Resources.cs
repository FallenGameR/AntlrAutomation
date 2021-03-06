﻿namespace Automation.Module.Tests.TestUtils
{
    public static class Resources
    {
        #region Full and Short

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

WS  : (' ' | '\t' )+ { $channel = Hidden; };

EOL : '\r'? '\n' { $channel = Hidden; };

BOL : '\u0000 BOL tokens are inserted via Emitter during lexing \u0000';

INDENT
    : '\u0000 INDENT tokens are inserted via Emitter during lexing \u0000';

DEDENT
    : '\u0000 DEDENT tokens are inserted via Emitter during lexing \u0000';
";

        public static string SampleShort =
@"
// Simple parser grammar

file   : section+ EOF -> ^(FILE section+);
section: 'start' WORD* 'end' -> ^(SECTION WORD*);

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
        #endregion

        #region Imaginary tokens

        public static string ImaginaryTokens =
@"
file: VARIABLE ID EOF -> ^(ANY_TOKEN VARIABLE ID);
VARIABLE
    : 'a'..'z'+;
ID  : '0'..'9'+;
";

        public static string ImaginaryTokensText = "name 42";

        #endregion

        #region Indents

        public static string Indents = @"
file: node+ EOF -> ^(ROOT node+);
node: ID (INDENT node+ DEDENT)? -> ^(ID node*);
ID  : ('a'..'z' | '_')+;
";

        public static string IndentsText = @"
root_a
    child_b
    child_c
    subroot_d
        child_e
        child_f
    child_g
";

        #endregion

        #region Emit

        public static string EmitBase = @"
file: ID ID EOF -> ^(BASE_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string EmitEol = @"
file: ID EOL ID EOF -> ^(EOL_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string EmitBol = @"
file: BOL ID BOL ID EOF -> ^(BOL_ROOT ID+);
ID  : 'a'..'z'+;";

        public static string EmitWs = @"
file: ID WS WS ID EOF -> ^(WHITESPACE_ROOT ID+);
ID  : 'a'..'z'+;
";

        public static string EmitIndent = @"
file:
    ID
    INDENT
        ID
    DEDENT
    EOF
    -> ^(INDENT_ROOT ID+);

ID  : 'a'..'z'+;
";

        public static string EmitText =
@"line
  indent";

        #endregion
    }
}

grammar (name, text, tokens) ::=
<<
grammar <name>;

options 
{
    language=CSharp3;
    output=AST;
    TokenLabelType=CommonToken;
    ASTLabelType=AutomationTree;
}

tokens {<tokens>}

@lexer::header{using Automation.Core;}
@parser::header{using Automation.Core;}
@lexer::namespace{<namespace(name=name)>}
@parser::namespace{<namespace(name=name)>}

/*
 * User Rules
 */
public <text>

/*
 * Pre-defined Lexer Rules
 */

WS  : (' ' | '\t' )+ { $channel = Hidden; };

NEWLINE
    : '\r'? '\n' { $channel = Hidden; };

INDENT
    : '\<%\$! INDENT tokens are inserted via IndentionGenerator during lexing !\$%\>';

DEDENT
    : '\<%\$! DEDENT tokens are inserted via IndentionGenerator during lexing !\$%\>';
>>


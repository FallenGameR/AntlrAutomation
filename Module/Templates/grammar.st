grammar (name, text) ::=
<<
grammar <name>;

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
@lexer::namespace{<namespace(name=name)>}
@parser::namespace{<namespace(name=name)>}

/*
 * User Rules
 */
public <text>

/*
 * Pre-defined Lexer Rules
 */

NEWLINE
    : '\r'? '\n' { $channel = Hidden; };

WS  : (' ' | '\t' )+ { $channel = Hidden; };
>>


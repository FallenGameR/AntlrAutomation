grammar (name, rules) ::=
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
@lexer::namespace{Automation.Parsers.<name>Grammar}
@parser::namespace{Automation.Parsers.<name>Grammar}

/*
 * User Rules
 */
public <rules>

/*
 * Pre-defined Lexer Rules
 */

NEWLINE
    : '\r'? '\n' { $channel = Hidden; };

WS	: (' ' | '\t' | NEWLINE)+ { $channel = Hidden; };
>>


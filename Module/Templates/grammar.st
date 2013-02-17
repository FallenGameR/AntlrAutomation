grammar (name, text, tokens, emitNewline, emitWhitespace) ::=
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
@lexer::namespace{<names/namespace(name=name)>}
@parser::namespace{<names/namespace(name=name)>}

/*
 * User Rules
 */
public <text>

/*
 * Pre-defined Lexer Rules
 */

WS  : (' ' | '\t' )+ <if(!emitWhitespace)>{ $channel = Hidden; }<endif>;

NEWLINE
    : '\r'? '\n' <if(!emitNewline)>{ $channel = Hidden; }<endif>;

INDENT
    : '\<%\$! INDENT tokens are inserted via IndentionGenerator during lexing !\$%\>';

DEDENT
    : '\<%\$! DEDENT tokens are inserted via IndentionGenerator during lexing !\$%\>';
>>


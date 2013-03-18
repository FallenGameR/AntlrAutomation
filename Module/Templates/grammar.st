grammar (name, text, tokens, emitEndOfLine, emitWhitespace) ::=
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

EOL : '\r'? '\n' <if(!emitEndOfLine)>{ $channel = Hidden; }<endif>;

BOL : '\<%\$! BOL tokens are inserted via Emitter during lexing !\$%\>';

INDENT
    : '\<%\$! INDENT tokens are inserted via Emitter during lexing !\$%\>';

DEDENT
    : '\<%\$! DEDENT tokens are inserted via Emitter during lexing !\$%\>';
>>


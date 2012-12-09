lexer grammar Grammar;
options {
  language=CSharp3;
  TokenLabelType=CommonToken;

}

@namespace {ParserLibrary}

T__9 : 'end' ;
T__10 : 'start' ;

// $ANTLR src "D:\Archive\Projects\AntlrAutomation\Module\..\Sample\ParserLibrary\Grammar.g3" 32
WORD: 'a'..'z'+;// $ANTLR src "D:\Archive\Projects\AntlrAutomation\Module\..\Sample\ParserLibrary\Grammar.g3" 34
NEWLINE
    : '\r'? '\n' { $channel = Hidden; };// $ANTLR src "D:\Archive\Projects\AntlrAutomation\Module\..\Sample\ParserLibrary\Grammar.g3" 37
WS	: (' ' | '\t' | NEWLINE)+ { $channel = Hidden; };
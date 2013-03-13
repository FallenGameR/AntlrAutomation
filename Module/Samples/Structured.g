file: section* -> ^(SECTIONS section*);

section: '[' ID ']' NEWLINE property* -> ^(ID property*);

property: name '=' value NEWLINE -> ^(name value);

name: ID;

value: .*;

COMMENTS: ';' .* '\n' { $channel = Hidden; };

ID: ('a'..'z' | 'A'..'Z' | '0'..'9' | '_' | '-' | '$' | '.' | '*')+;

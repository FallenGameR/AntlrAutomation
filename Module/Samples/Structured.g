file: section* -> ^(SECTIONS section*);

section: '[' ID ']' NEWLINE property* -> ^(ID property*);
property: name '=' value NEWLINE-> ^(name value);
name: ID;
value: ~NEWLINE;

ID: ('a'..'z' | 'A'..'Z' | '0'..'9' | '_' | '-' | '$' | '.' | '*')+;
COMMENTS: ';' .* NEWLINE { $channel = Hidden; };


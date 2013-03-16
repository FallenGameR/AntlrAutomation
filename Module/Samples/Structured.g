file: section* -> ^(SECTIONS section*);

section: BOL '[' ID ']' property* -> ^(ID property*);
property: BOL name '=' value -> ^(name value);
name: ID;
value: ~BOL;

ID: ('a'..'z' | 'A'..'Z' | '0'..'9' | '_' | '-' | '$' | '.' | '*')+;
COMMENTS: ';' .* '\n' { $channel = Hidden; };


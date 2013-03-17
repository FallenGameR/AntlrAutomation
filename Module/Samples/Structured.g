file: section* -> ^(SECTIONS section*);

section: BOL '[' ID ']' EOL property* -> ^(ID property*);
property: BOL name '=' value EOL-> ^(name value);
name: ID;
value: ~EOL;

ID: ('a'..'z' | 'A'..'Z' | '0'..'9' | '_' | '-' | '$' | '.' | '*')+;
COMMENTS: BOL ';' .* EOL { $channel = Hidden; };


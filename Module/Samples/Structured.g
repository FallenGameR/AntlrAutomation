file: section*;

section: '[' ID ']' NEWLINE property*;

property: name '=' value NEWLINE;

name: ID;

value: .*;

COMMENTS: '!' (~NEWLINE) NEWLINE;

ID: 'a'..'z' | 'A'..'Z' | '0'..'9' | '_' | '-' | '$' | '.' | '*';

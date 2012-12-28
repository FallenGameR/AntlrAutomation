$fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
Set-Grammar $fullText
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple

function Get-Grammar
{
    <#
    .ABSTRACT
        Get grammar object for specified grammar

    .DESCRIPTION
        Object properties:
        - Name
        - Folder
        - Assembly
        - FullText
        - ShortText
        + Parse() that can handle AppDomain reuse in filter scenario
        + Save() that dumps dll and all references to a folder
    #>
}

filter Get-Node
{
    <#
    .ABSTRACT
        Get corresponding node(s) in the AST

    .DESCRIPTION
        Finds all nodes with name specified via regex.
        -Recurse would traverse whole input tree
        -OnlyChildren would return only children, without returning matching nodes themselves
        -OnlyTokens would return only matching token names, without returning the values

        Can accept multiple nodes as input.

        Alias: Get-Tree, Where-Tree, Where-Node

        Uses methods defined in AutomationTree. That way we could use the same Get-Node functionality from C# project that uses the same genrated parser.
    #>
}


<#
Render trees simplified - text + children

Allow syntax like: FILE.some.sub.child (expand for all found children, discard the onces that don not have this path)?

Predefined samples:
- ini
- csv
- ping
- tracert
- whois
- diff?
- git?
- sd?
- logs?
- cs? (fuzzy)

Error handling:
- missing/extra token that is treated as recoverable error is a warning
- parser error recovery via "recovery stack" is treated as an error
- NoViableAltExceptions is as much verbose as possible
- Compilation warnings to warnings
- Compilation errors to errors. The grammar is discarded since it is not valid.
#>

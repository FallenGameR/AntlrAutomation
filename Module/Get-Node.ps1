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


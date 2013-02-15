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
        CloseDomain
    #>
}

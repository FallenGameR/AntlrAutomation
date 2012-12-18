function Set-Grammar
{
    <#
    .ABSTRACT
        Defines grammar object by grammar text

    .DESCRIPTION
        Should operate on both full and short text definitions.
        -Force switch to overwrite existing grammars.
        - Would search for referenced tokens and grammar name.
        - Would handle compilation and cleanup.
        - Would handle md5 grammar text check.
        - Would handle namespace collisions.
    #>

    param
    (
        [string] $FullText
    )

    $nameFound = $fullText -match "grammar (\w+);"
    if( -not $nameFound )
    {
        throw "Couldn't locate grammar name in full grammar text"
    }

    $name = $Matches[1]

}


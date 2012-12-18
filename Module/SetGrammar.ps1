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

    $name = Get-ParserName $fullText

    Clean-ParserFolder $name
    Set-GrammarText $name $fullText
    Generate-Parser $name
    Compile-Parser $name
}

function Get-ParserName( [string] $fullText )
{
    $nameFound = $fullText -match "grammar (\w+);"

    if( -not $nameFound )
    {
        throw "Couldn't locate grammar name in full grammar text"
    }

    $Matches[1]
}

function Clean-ParserFolder( [string] $name )
{
    $parserFolder = Get-ParserFolder $name

    if( Test-Path $parserFolder )
    {
        Remove-Item $parserFolder -Recurse
    }

    New-Item (Get-ParserFolder $name) -ItemType Directory | Out-Null
    New-Item (Get-ParserSourceFolder $name) -ItemType Directory | Out-Null
    New-Item (Get-ParserBinaryFolder $name) -ItemType Directory | Out-Null
}

function Set-GrammarText( [string] $name, [string] $fullText )
{
    $fullText | Set-Content (Get-FullGrammarPath $name)
}

function Generate-Parser( [string] $name )
{
    Generate-ParserCore $name
    Generate-ParserUtils $name
}

function Generate-ParserCore( [string] $name )
{
    $param =
        "-o", (Get-ParserSourceFolder $name),
        "-language", "CSharp3",
        (Get-FullGrammarPath $name)

    & (Get-AntlrExe) $param
}

function Generate-ParserUtils( [string] $name )
{
    $allTemplates = Join-Path (Get-TemplatesRoot) "*.cs"
    Copy-Item $allTemplates (Get-ParserSourceFolder $name)
}

function Compile-Parser( [string] $name )
{
    $param =
        "/nologo",
        "/optimize",
        "/target:library",
        "/out:$(Get-ParserAssemblyPath $name)",
        "/reference:$(Get-AntlrRuntimeDll),$(Get-AutomationCoreDll)",
        "$(Get-ParserSourceFolder $name)\*.cs"

    & (Get-CompilerExe) $param
}

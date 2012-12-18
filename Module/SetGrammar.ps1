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

    # Preparing grammar folder and file
    $name = Get-ParserName $fullText
    $parserFolder = Get-ParserFolder $name

    if( Test-Path $parserFolder )
    {
        #Remove-Item $parserFolder
    }

    $fullGrammarPath = Get-GrammarFullTextPath $name
    $fullText | Set-Content $fullGrammarPath

    # Generating parser
    $antlr = Get-AntlrExe
    $param =
        "-o", (Get-GrammarSourceFolder $name),
        "-language", "CSharp3",
        $fullGrammarPath
    & $antlr $param

    # Compiling parser
    $compiler = Get-CompilerExe
    $param =
        "/nologo",
        "/optimize",
        "/target:library",
        "/out:$(Get-GrammarAssemblyPath $name)",
        "/reference:$(Get-AntlrRuntimeDll),$(Get-AutomationCoreDll)",
        "$(Get-GrammarSourceFolder $name)\*.cs"
    & $compiler $param
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

function Get-ParserFolder( [string] $name )
{
    Join-Path (Get-ParsersRoot) $name
}

function Get-LibraryFolder( [string] $name )
{
    Join-Path (Get-LibrariesRoot) $name
}

function Get-AntlrExe
{
    Join-Path (Get-LibraryFolder Antlr) Antlr3.exe
}

function Get-AntlrRuntimeDll
{
    Join-Path (Get-LibraryFolder Antlr) Antlr3.Runtime.dll
}

function Get-AutomationCoreDll
{
    Join-Path (Get-LibraryFolder AutomationCore) Automation.Core.dll
}

function Get-CompilerExe
{
    Join-Path ([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()) csc.exe
}

function Get-GrammarFullTextPath( [string] $name )
{
    Join-Path (Get-ParserFolder $name) ($name + ".g3")
}

function Get-GrammarSourceFolder( [string] $name )
{
    Join-Path (Get-ParserFolder $name) src
}

function Get-GrammarAssemblyPath( [string] $name )
{
    $folder = Join-Path (Get-ParserFolder $name) bin
    Join-Path $folder parser.dll
}

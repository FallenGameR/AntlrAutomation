
$librariesRoot = Join-Path $PSScriptRoot Libraries
$parsersRoot = Join-Path $PSScriptRoot Parsers

function Get-LibrariesRoot { $librariesRoot }
function Get-ParsersRoot { $parsersRoot }




function Test-Parser
{
    $root = $PSScriptRoot
    $antlrRoot = "$root\Libraries\Antlr"
    $automationRoot = "$root\Libraries\AutomationCore"
    $grammarRoot = "$root\Parsers\Grammar"

    $antlr = Join-Path $antlrRoot "Antlr3.exe"
    $csc = Join-Path ([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()) "csc.exe"

    $param =
        "-o", "$grammarRoot\src\",
        "-language", "CSharp3",
        "$grammarRoot\Grammar.g3"
    & $antlr $param

    $param =
        "/nologo",
        "/optimize",
        "/target:library",
        "/out:$grammarRoot\bin\parser.dll",
        "/reference:$antlrRoot\Antlr3.Runtime.dll,$automationRoot\Automation.Core.dll",
        "$grammarRoot\src\*.cs"
    & $csc $param

    # App domain is needed to be able to change parser.dll withing one Powershell session
    # We need to make sure that new domain would be able to find all referenced assemblies
    $setup = New-Object AppDomainSetup
    $setup.ApplicationBase = "$root\Libraries\"
    $setup.PrivateBinPath = "Antlr;AutomationCore"
    $evidence = [AppDomain]::CurrentDomain.Evidence
    $parserDomain = [AppDomain]::CreateDomain( "GrammarParserDomain", $evidence, $setup )

    # Preaparing the loader for the parsing
    $loader = $parserDomain.CreateInstanceFromAndUnwrap( "$grammarRoot\bin\parser.dll", "Sample.Parser.Loader" )
    $filePath = "$root\..\Info\simpleton.txt"

    # Transparent proxy cast is not working in Powershell, explicitly call Parse method via reflection
    $tree = [Automation.Core.ILoader].GetMethod("Parse").Invoke($loader, $filePath)
    [AppDomain]::Unload($parserDomain)

    $tree.ToStringTree()
    #$tree.Children[0].Children
}


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

filter Parse-Item
{
    <#
    .ABSTRACT
        Parse file(s) with a grammar

    .DESCRIPTION
        Takes grammar name or full/short text to construct AST for each input file.
        -Parallel runs parsing in parallel threads
        -Tee would output input files to console and return AST as usual

        Alias: Parse-File
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

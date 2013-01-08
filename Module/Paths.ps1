$SCRIPT:librariesRoot = Join-Path $PSScriptRoot Libraries
$SCRIPT:parsersRoot = Join-Path $PSScriptRoot Parsers
$SCRIPT:templatesRoot = Join-Path $PSScriptRoot Templates

function Get-LibrariesRoot
{
    $SCRIPT:librariesRoot
}

function Get-ParsersRoot
{
    $SCRIPT:parsersRoot
}

function Get-TemplatesRoot
{
    $SCRIPT:templatesRoot
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

function Get-LibraryFolder( [string] $name )
{
    Join-Path (Get-LibrariesRoot) $name
}

function Get-ParserName( [string] $name )
{
    (Get-Item (Get-ParserFolder $name -LooseMatch)).Name
}

function Get-ParserFolder
{
    param
    (
        # Name of the parser
        [string] $Name,
        # Perform loose regex-based name matching
        [switch] $LooseMatch
    )

    # Loose match is needed
    if( $LooseMatch )
    {
        $parserFolders = Get-ChildItem (Get-ParsersRoot)

        # Exact match for an existing parser
        $findings = $parserFolders | where Name -eq $name
        if( $findings )
        {
            return $findings.FullName
        }

        # Regex match for an existing parser
        $findings = $parserFolders | where Name -match $name
        if( $findings )
        {
            if( ($findings | measure).Count -gt 1 )
            {
                $names = ($findings | foreach Name) -join ", "
                Write-Warning "Grammar '$name' can be resolved as: $names. First grammar would be used."
            }
            return ($findings | select -First 1).FullName
        }
    }

    # Strict match is needed
    Join-Path (Get-ParsersRoot) $name
}

function Get-FullGrammarPath( [string] $name )
{
    $file = $name + ".g3"
    Join-Path (Get-ParserFolder $name) $file
}

function Get-ParserSourceFolder( [string] $name )
{
    Join-Path (Get-ParserFolder $name) src
}

function Get-ParserBinaryFolder( [string] $name )
{
    Join-Path (Get-ParserFolder $name) bin
}

function Get-ParserAssemblyPath( [string] $name )
{
    Join-Path (Get-ParserBinaryFolder $name) "$($name)Parser.dll"
}

function Get-ParserSourceFile( [string] $name, [string] $file )
{
    $folder = Get-ParserSourceFolder $name
    Join-Path $folder $file
}

filter Set-ParserSourceFile( [string] $name, [string] $file )
{
    $path = Get-ParserSourceFile $name $file
    $psitem | Set-Content $path
}


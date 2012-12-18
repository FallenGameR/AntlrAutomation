$SCRIPT:librariesRoot = Join-Path $PSScriptRoot Libraries
$SCRIPT:parsersRoot = Join-Path $PSScriptRoot Parsers

function Get-LibrariesRoot
{
    $SCRIPT:librariesRoot
}

function Get-ParsersRoot
{
    $SCRIPT:parsersRoot
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

function Get-ParserFolder( [string] $name )
{
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

function Get-ParserAssemblyPath( [string] $name )
{
    $folder = Join-Path (Get-ParserFolder $name) bin
    Join-Path $folder parser.dll
}

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


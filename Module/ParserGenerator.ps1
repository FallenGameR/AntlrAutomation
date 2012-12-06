function Test-Parser
{
    $antlrPath = "$PSScriptRoot\..\Libraries\antlr-3.4.1.9004\"
    $antlr = Join-Path $antlrPath "antlr3.exe"
    $csc = Join-Path ([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()) "csc.exe"

    $param = , "$PSScriptRoot\..\Sample\ParserLibrary\Grammar.g3"
    & $antlr $param

    $param =
        "/nologo",
        "/optimize",
        "/target:library",
        "/out:parser.dll",
        "/lib:$antlrPath",
        "/reference:Antlr3.Runtime.dll",
        "$PSScriptRoot\..\Sample\ParserLibrary\*.cs"
    & $csc $param

    [Reflection.Assembly]::LoadFrom( (Join-Path (pwd) "parser.dll") ) | Out-Null

    $parser = New-Object ParserLibrary.Loader
    $tree = $parser.Parse( "$PSScriptRoot\..\Sample\Resources\simpleton.txt" )
    $tree
}
#$tree.Children[0].Children

<#
Module architecture.

Multiple parsers can be generated in the same Powershell session.

#Render trees simplified - text + children
#Allow syntax like:
#- get all 'FILE' children +recursive
#- FILE.some.sub.child (expand for all found children, discard the onces that don not have this path)?

Automatic temp file cleanup.

Parser caching.

Simplified grammars on input.

Grammar error handling.

Predefined grammars for fuzzy cs filter, csv and ini.
#>

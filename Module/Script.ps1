

$antlrPath = "d:\Archive\Projects\AntlrAutomation\Libraries\antlr-3.4.1.9004\"
$antlr = Join-Path $antlrPath "antlr3.exe"
$csc = Join-Path ([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()) "csc.exe"

$param = , ".\Grammar.g3"
& $antlr $param

$param =
    "/nologo",
    "/optimize",
    "/target:library",
    "/out:parser.dll",
    "/lib:$antlrPath",
    "/reference:Antlr3.Runtime.dll",
    "*.cs"
& $csc $param

[Reflection.Assembly]::LoadFrom( (Join-Path $antlrPath "Antlr3.Runtime.dll") )
[Reflection.Assembly]::LoadFrom( "parser.dll" )

$parser = New-Object ParserLibrary.Loader
$tree = $parser.Parse( "d:\Archive\Projects\AntlrAutomation\Sample\Resources\simpleton.txt" )

#$tree.Children[0].Children
#Render trees simplified - text + children
#Allow syntax like:
#- get all 'FILE' children +recursive
#- FILE.some.sub.child (expand for all found children, discard the onces that don not have this path)?


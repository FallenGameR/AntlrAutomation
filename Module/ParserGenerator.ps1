#function Test-Parser
#{
    $root = $PSScriptRoot
    $antlrRoot = "$root\Libraries\Antlr"
    $automationRoot = "$root\Libraries\AutomationCore"
    $grammarRoot = "$root\Parsers\Grammar"

    $antlr = Join-Path $antlrRoot "Antlr3.exe"
    $csc = Join-Path ([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()) "csc.exe"

    $param =
        "-o", "$grammarRoot\src\",
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

    $host.EnterNestedPrompt()

    [Reflection.Assembly]::LoadFrom( (Join-Path (pwd) "InterfaceLibrary.dll") ) | Out-Null


    $parserDomain = [AppDomain]::CreateDomain( "ParserDomain" )
    # Transparent proxy cast is not working in Powershell
    $loader = $parserDomain.CreateInstanceFromAndUnwrap( "parser.dll", "ParserLibrary.Loader" )
    $filePath = "$root\..\Sample\Resources\simpleton.txt"
    $tree = [InterfaceLibrary.ILoader].GetMethod("Parse").Invoke($loader, $filePath)
    [AppDomain]::Unload($parserDomain)

    $tree.ToStringTree()


#}
#$tree.Children[0].Children







<#
Multiple parsers can be generated in the same Powershell session.

Source files are taken from the module folder itself.

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

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
}
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

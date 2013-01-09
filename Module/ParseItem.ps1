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

    param
    (
        [string] $name
    )

    $filePath = (Get-Item $psitem).FullName

    # App domain is needed to be able to change parser assemby without closing current Powershell session
    # We need to make sure that new domain would be able to find all referenced assemblies
    $setup = New-Object AppDomainSetup
    $setup.ApplicationBase = Get-LibrariesRoot
    $setup.PrivateBinPath = (Get-ChildItem (Get-LibrariesRoot) | where PSIsContainer | foreach Name) -join ";"
    $evidence = [AppDomain]::CurrentDomain.Evidence
    $domainName = $name + "ParserDomain"
    $parserDomain = [AppDomain]::CreateDomain( $domainName, $evidence, $setup )

    # Preaparing the loader for the parsing
    $parserName = Get-ParserName $name
    $dllPath = Get-ParserAssemblyPath $parserName
    $namespace = "Automation.Parsers.$($parserName).$($parserName)Loader" # NOTE: Capitalization issue here
    $loader = $parserDomain.CreateInstanceFromAndUnwrap( $dllPath, $namespace )

    # Transparent proxy cast is not working in Powershell, explicitly call Parse method via reflection
    $tree = [Automation.Core.ILoader].GetMethod("Parse").Invoke($loader, $filePath)
    [AppDomain]::Unload($parserDomain)

    # Return parsed AST
    $tree
}


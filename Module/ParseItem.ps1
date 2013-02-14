function Parse-Item
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
        [string] $Name,
        [string] $FilePath,
        [switch] $Tokens
    )

    $filePath = (Get-Item $filePath).FullName
    $name = Get-ParserName $name

    # App domain is needed to be able to change parser assemby without closing current Powershell session
    # We need to make sure that new domain would be able to find all referenced assemblies
    $setup = New-Object AppDomainSetup
    $setup.ApplicationBase = Get-LibrariesRoot
    $setup.PrivateBinPath = (Get-ChildItem (Get-LibrariesRoot) | where PSIsContainer | foreach Name) -join ";"
    $evidence = [AppDomain]::CurrentDomain.Evidence
    $domainName = Get-Render domainName name
    $parserDomain = [AppDomain]::CreateDomain( $domainName, $evidence, $setup )

    # Preaparing the loader for the parsing
    $dllPath = Get-ParserAssemblyPath $name
    $loaderFullName = "{0}.{1}" -f (Get-Render namespace name), (Get-Render loaderName name)
    $loader = $parserDomain.CreateInstanceFromAndUnwrap( $dllPath, $loaderFullName )

    # Get the tokens from the lexer or AST from the parser
    $result = if( $Tokens )
    {
        Parse-Tokens $loader $filePath
    }
    else
    {
        Parse-Tree $loader $filePath
    }

    # Cleanup and return the result
    [AppDomain]::Unload($parserDomain)
    $result
}

function Parse-Tree( $loader, $filePath )
{
    # Transparent proxy cast is not working in Powershell, explicitly call Parse method via reflection
    [Automation.Core.ILoader].GetMethod("Parse").Invoke($loader, $filePath)
}

function Parse-Tokens( $loader, $filePath )
{
    # Does not actually return something, all output is printed in color to console
    $tokens = [Automation.Core.ILoader].GetMethod("Tokenize").Invoke($loader, $filePath)
    $tokens | Colorize-Token
}

filter Colorize-Token
{
    switch -regex ( $psitem )
    {
        "^\s*<[A-Z_]+>\s*$"
        {
            Write-Console -NoNewLine -F DarkGreen $psitem
        }
        "^\s*-+\s*$"
        {
            Write-Console -NoNewLine -F DarkCyan $psitem
        }
        default
        {
            Write-Console -NoNewLine $psitem
        }
    }
}

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

    $text = Get-Content $filePath | Out-String
    $name = Get-ParserName $name

    # App domain is needed to be able to change parser assemby without closing current Powershell session
    # We need to make sure that new domain would be able to find all referenced assemblies
    $setup = New-Object AppDomainSetup
    $setup.ApplicationBase = Get-LibrariesRoot
    $setup.PrivateBinPath = (Get-ChildItem (Get-LibrariesRoot) | where PSIsContainer | foreach Name) -join ";"
    $evidence = [AppDomain]::CurrentDomain.Evidence
    $domainName = Render-Template names/domain
    $parserDomain = [AppDomain]::CreateDomain( $domainName, $evidence, $setup )

    # Preparing the loader for the parsing
    $dllPath = Get-ParserAssemblyPath $name
    $loaderFullName = "{0}.{1}" -f (Render-Template names/namespace), (Render-Template names/loader)
    $loader = $parserDomain.CreateInstanceFromAndUnwrap( $dllPath, $loaderFullName )

    # Get the tokens from the lexer or AST from the parser
    $result = if( $Tokens )
    {
        Parse-Tokens $loader $text
    }
    else
    {
        Parse-Tree $loader $text
    }

    # Cleanup and return the result
    [AppDomain]::Unload($parserDomain)
    $result
}

function Parse-Tree( $loader, $text )
{
    # Transparent proxy cast is not working in Powershell, explicitly call Parse method via reflection
    [Automation.Core.ILoader].GetMethod("Parse").Invoke($loader, $text)
}

function Parse-Tokens( $loader, $filePath )
{
    # Does not actually return something, all output is printed in color to console
    $tokens = [Automation.Core.ILoader].GetMethod("Tokenize").Invoke($loader, $text)
    $tokens | Colorize-Token
}

filter Colorize-Token
{
    function Write-Console( [object] $Object, [string] $ForegroundColor )
    {
        $original = [Console]::ForegroundColor

        if( $foregroundColor )
        {
            [Console]::ForegroundColor = [ConsoleColor]::$ForegroundColor
        }

        [Console]::Write( $object -join ' ' )
        [Console]::ForegroundColor = $original
    }

    switch -regex ( $psitem )
    {
        "^\s*\[[A-Z_]+\]\s*$"
        {
            Write-Console $psitem DarkYellow
        }
        "^\s*<[A-Z_]+>\s*$"
        {
            Write-Console $psitem DarkGreen
        }
        "^\s*-+\s*$"
        {
            Write-Console $psitem DarkCyan
        }
        default
        {
            Write-Console $psitem
        }
    }
}

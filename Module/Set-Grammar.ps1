$SCRIPT:Templates = New-Object Antlr4.StringTemplate.TemplateGroupDirectory (Get-TemplatesRoot)

function Set-Grammar
{
    <#
    .ABSTRACT
        Defines grammar object by grammar text

    .DESCRIPTION
        Should operate on both full and short text definitions.
        -Force switch to overwrite existing grammars.
        - Would search for referenced tokens and grammar name.
        - Would handle compilation and cleanup.
        - Would handle md5 grammar text check.
        - Would handle namespace collisions.

        name is taken
        - grammar name in the file
        - file name (no name in the file) short;

        text is taken
        - from the file
        - generated via template (short)
    #>

    param
    (
        [Parameter(Mandatory = $true)]
        [string] $GrammarPath,
        [switch] $EmitIndents,
        [switch] $EmitNewline,
        [switch] $EmitWhitespace
    )

    # Control flags
    $SCRIPT:EmitIndents = [bool] $EmitIndents
    $SCRIPT:EmitNewline = [bool] $EmitNewline
    $SCRIPT:EmitWhitespace = [bool] $EmitWhitespace

    # Generating grammar
    $name, $text = Read-Grammar $grammarPath
    Clean-ParserFolder $name
    Set-GrammarFile $name $text
    Generate-Parser $name
    Compile-Parser $name
}

function Read-Grammar( [string] $path )
{
    $text = Get-Content $path | Out-String
    $nameFound = $text -match "grammar (\w+);"

    if( $nameFound )
    {
        # Full text grammar
        $name = $matches[1]
    }
    else
    {
        # Short text gramar
        $name = (Get-Item $path).BaseName
        $tokens = (Get-ImaginaryTokens $text) -join " "
        $text = Render-Template grammar
    }

    $name, $text
}

function Get-ImaginaryTokens( [string] $text )
{
    $tokenNames = ([regex] "[A-Z_]+").Matches($text).Value
    $exceptNames = ([regex] "(?m)^[A-Z_]+").Matches($text).Value + "EOF"

    $tokenSet = New-Object Collections.Generic.HashSet[string] (, [string[]] $tokenNames)
    $exceptSet = New-Object Collections.Generic.HashSet[string] (, [string[]] ($exceptNames))
    $tokenSet.ExceptWith( $exceptSet )
    $tokenSet | sort | foreach{ "$psitem; " }
}

function Clean-ParserFolder( [string] $name )
{
    $parserFolder = Get-ParserFolder $name

    if( Test-Path $parserFolder )
    {
        Remove-Item $parserFolder -Recurse
    }

    New-Item (Get-ParserFolder $name) -ItemType Directory | Out-Null
    New-Item (Get-ParserSourceFolder $name) -ItemType Directory | Out-Null
    New-Item (Get-ParserBinaryFolder $name) -ItemType Directory | Out-Null

    Write-Verbose "Folder '$parserFolder' is cleaned for parser '$name'"
}

function Set-GrammarFile( [string] $name, [string] $fullText )
{
    $fullText | Set-Content (Get-FullGrammarPath $name)

    Write-Verbose "Grammar file set for parser '$name'"
}

function Generate-Parser( [string] $name )
{
    Generate-ParserCore $name
    Generate-ParserExtensions $name

    Write-Verbose "Sources generated for parser '$name'"
}

function Compile-Parser( [string] $name )
{
    $param =
        "/nologo",
        "/optimize",
        "/target:library",
        "/out:$(Get-ParserAssemblyPath $name)",
        "/reference:$(Get-AntlrRuntimeDll),$(Get-AutomationCoreDll)",
        "$(Get-ParserSourceFolder $name)\*.cs"

    & (Get-CompilerExe) $param

    Write-Verbose "Binaries compiled for parser '$name'"
}

function Generate-ParserCore( [string] $name )
{
    $param =
        "-o", (Get-ParserSourceFolder $name),
        "-language", "CSharp3",
        (Get-FullGrammarPath $name)

    & (Get-AntlrExe) $param
}

function Generate-ParserExtensions( [string] $name )
{
    Render-Template assemblyInfo| Set-ParserSourceFile $name "$($name)AssemblyInfo.cs"
    Render-Template loader      | Set-ParserSourceFile $name "$($name)Loader.cs"
    Render-Template lexer       | Set-ParserSourceFile $name "$($name)Lexer.Partial.cs"
    Render-Template parser      | Set-ParserSourceFile $name "$($name)Parser.Patial.cs"
}

function Render-Template( [string] $templateName )
{
    $template = $templates.GetInstanceOf($templateName)

    foreach( $attribute in $template.GetAttributes().Keys )
    {
        $value = (Get-Variable $attribute).Value
        $template.Add($attribute, $value) | Out-Null
    }

    $template.Render()
}


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
        - explicit name (no name in the file) short; if there is a name in file, warning
        - file name (no name in the file) short;

        text is taken
        - from the file
        - generated via template (short)

        analyse verbose output in tests
    #>

    param
    (
        [Parameter(Mandatory = $true)]
        [string] $FilePath,
        [string] $Name
    )

    $text = Get-Content $filePath | Out-String

    if( -not $name )
    {
        $nameFound = $text -match "grammar (\w+);"
        $name = $Matches[1]
        if( $name )
        {
            $fullText = $true
        }
    }
    if( -not $name )
    {
        $name = (Get-Item $filePath).BaseName
    }

    Clean-ParserFolder $name
    Generate-Grammar $fullText $name $text
    Generate-Parser $name
    Compile-Parser $name
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

    Write-Verbose "Parser folder '$parserFolder' is cleaned"
}

function Generate-Grammar( [bool] $fullText, [string] $name, [string] $text )
{
    if( -not $fullText )
    {
        $text = Get-Render grammar name text
    }
    Set-GrammarText $name $text
}

function Set-GrammarText( [string] $name, [string] $fullText )
{
    $fullText | Set-Content (Get-FullGrammarPath $name)

    Write-Verbose "Grammar text is set for parser '$name'"
}

function Generate-Parser( [string] $name )
{
    Generate-ParserCore $name
    Generate-ParserExtensions $name

    Write-Verbose "Parser sources generated for parser '$name'"
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

    Write-Verbose "Parser binaries compiled for parser '$name'"
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
    Get-Render assemblyInfo name     | Set-ParserSourceFile $name "$($name)AssemblyInfo.cs"
    Get-Render loader name           | Set-ParserSourceFile $name "$($name)Loader.cs"
    Get-Render lexerExtensions name  | Set-ParserSourceFile $name "$($name)Lexer.Partial.cs"
    Get-Render parserExtensions name | Set-ParserSourceFile $name "$($name)Parser.Patial.cs"
}

function Get-Render( [string] $templateName )
{
    $template = $templates.GetInstanceOf($templateName)

    foreach( $arg in $args )
    {
        $value = (Get-Variable $arg).Value
        $template.Add($arg, $value) | Out-Null
    }

    $template.Render()
}


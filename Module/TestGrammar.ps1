function Run-Tests
{
    # Removing old compiled grammars for clear test run
    del "$PSScriptRoot\Parsers\Simpleton" -Recurse
    del "$PSScriptRoot\Parsers\SimpletonCopy" -Recurse

    # Simpleton grammar compiles without errors
    $fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
    Set-Grammar $fullText

    # Parse-Item works with exact grammar name
    "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item Simpleton

    # Parse-Item works with exact grammar name case insensetive
    "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simpleton

    # Parse-Item works with regex matched grammar name
    "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple

    # SimpletonCopy grammar compiles without errors despite it's partial name overlap
    $fullTextCopy = type "$PSScriptRoot\..\Info\simpletonCopy.g3" | Out-String
    Set-Grammar $fullTextCopy

    # Parse-Item works with regex matched grammar name with warning in case of ambiguity
    "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple
}

Update-FormatData -AppendPath .\AntlrAutomation.ps1xml

function test( [switch] $Force )
{
    if( $Force )
    {
        $fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
        Set-Grammar $fullText
    }
    $GLOBAL:a = "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item Simpleton
    Update-FormatData
    $a | fc
}

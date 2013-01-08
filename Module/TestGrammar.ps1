
function Run-Tests
{
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
    # TODO: Right now the test fails - grammar can't be compiled since namespace generated is wrong
    $fullTextCopy = type "$PSScriptRoot\..\Info\simpletonCopy.g3" | Out-String
    Set-Grammar $fullTextCopy

    # Parse-Item works with regex matched grammar name with warning in case of ambiguity
    # TODO: Right now this test doesn't capture warning output at all
    "$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple
}

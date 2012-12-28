# Simpleton grammar compiles without errors
$fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
Set-Grammar $fullText

# Parse-Item works with exact grammar name
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item Simpleton

# Parse-Item works with exact grammar name case insensetive
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simpleton

# Parse-Item works with regex matched grammar name
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple

# Parse-Item works with regex matched grammar name with warning in case of ambiguity
$fullTextCopy = type "$PSScriptRoot\..\Info\simpletonCopy.g3" | Out-String
Set-Grammar $fullTextCopy
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple

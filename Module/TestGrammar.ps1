
# Simpleton grammar compiles without errors
# Parse-Item works with exact grammar name
# Parse-Item works with exact grammar name case insensetive
# Parse-Item works with regex matched grammar name
# Parse-Item works with regex matched grammar name with warning in case of ambiguity
#
$fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
Set-Grammar $fullText
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple


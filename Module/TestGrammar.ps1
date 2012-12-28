$fullText = type "$PSScriptRoot\..\Info\simpleton.g3" | Out-String
Set-Grammar $fullText
"$PSScriptRoot\..\Info\simpleton.txt" | Parse-Item simple


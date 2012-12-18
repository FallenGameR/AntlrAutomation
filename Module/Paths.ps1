$SCRIPT:librariesRoot = Join-Path $PSScriptRoot Libraries
$SCRIPT:parsersRoot = Join-Path $PSScriptRoot Parsers

function Get-LibrariesRoot { $SCRIPT:librariesRoot }
function Get-ParsersRoot { $SCRIPT:parsersRoot }

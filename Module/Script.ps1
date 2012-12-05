antlr3.exe .\Simpleton.g3
$env:path += ";$([Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory())"
csc.exe /nologo /optimize /out:parser.exe '/lib:d:\Archive\Projects\AntlrAutomation\Libraries\antlr-3.4.1.9004\' /reference:Antlr3.Runtime.dll *.cs .\Properties\AssemblyInfo.cs
copy d:\Archive\Projects\AntlrAutomation\Libraries\antlr-3.4.1.9004\Antlr3.Runtime.dll .\Antlr3.Runtime.dll
.\parser.exe


<#
Build dll
Return tree from it
dll code should span one single file

all referenced dlls must be embedded as a resource
#>

[CmdletBinding()]
param (
    [ValidateScript({Test-Path $_ -PathType Container})]
    [parameter(Mandatory=$false)]
    $NugetFileShare = "C:\dev\nuget"
)

function Get-NextVersion($VersionFile)
{
    # Work out the version number
    $nextVersion = Get-Content $VersionFile -ErrorAction Stop
    if ($null -eq $nextVersion)
    {
        Write-Error "The $VersionFile file is empty"
        Exit 1
    }
    if ($nextVersion.GetType().BaseType.Name -eq "Array")
    {
        $nextVersion = $nextVersion[0]
        Write-Warning "$VersionFile contains more than one line of text. Using the first line."
    }
    if ($nextVersion -notmatch "^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$")
    {
        Write-Error "The contents of $VersionFile (`"$nextVersion`") not recognised as a valid version number."
        Exit 2
    }
    
    return $nextVersion;
}

function Get-PackageVersionSuffix()
{
    $now = Get-Date
    $datePart = $now.ToString("yyMMdd.HHmm");
    return "lp.$datePart";
}

$VersionFile = "$PSScriptRoot/version.txt";
$STRAVAIG_SOLUTION = "$PSScriptRoot/src/Stravaig.Configuration.Diagnostics.sln"
$STRAVAIG_PACKAGE_VERSION = Get-NextVersion -VersionFile $VersionFile;
$STRAVAIG_PACKAGE_VERSION_SUFFIX = Get-PackageVersionSuffix;

Get-ChildItem ./out -Recurse | Remove-Item;

dotnet pack $STRAVAIG_SOLUTION --configuration Release --output ./out --include-symbols --include-source /p:VersionPrefix="$STRAVAIG_PACKAGE_VERSION" --version-suffix "$STRAVAIG_PACKAGE_VERSION_SUFFIX" -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg

Get-ChildItem ./out/*.nupkg | ForEach-Object {
    $name = $_.FullName;
    Write-Output "Pushing $name";
    dotnet nuget push "$name" --source $NugetFileShare
}

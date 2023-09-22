# Check if .NET Core 7 or later is installed
$dotnetVersion = (dotnet --version)
$requiredDotNetVersion = "7.0.0"

Write-Output "Checking dotnet version."
if ([Version]::new($dotnetVersion) -lt [Version]::new($requiredDotNetVersion)) {
    Write-Warning "You need .NET Core $requiredDotNetVersion or later installed on your system."
    exit 1
}

# Check if EF Core tools are installed
$efToolsVersion = (dotnet ef --version)

Write-Output "Checking EF tools are installed."
if ($efToolsVersion -eq $null) {
    Write-Warning "Entity Framework Core tools are not installed. Please install them using 'dotnet tool install --global dotnet-ef'."
    exit 1
}

$scriptDirectory = [System.IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path)
$targetDirectory = Join-Path $scriptDirectory "src\SimpleBlazorGrid.EntityFramework.Sandbox.Core"

Write-Output "Applying migrations..."
if (Test-Path -Path $targetDirectory -PathType Container) {
    Set-Location -Path $targetDirectory
} else {
    Write-Warning "The target directory $targetDirectory does not exist."
    exit 1
}

# Perform database migration
$commandToRun = "dotnet ef database update -s ..\SimpleBlazorGrid.EntityFramework.Sandbox"
Invoke-Expression -Command $commandToRun

# Change directory to the current user's AppData\Local directory
$userAppDataLocalDirectory = [System.Environment]::GetFolderPath([System.Environment+SpecialFolder]::LocalApplicationData)
Set-Location -Path $userAppDataLocalDirectory

# Check for the existence of "simpleblazorgrid.db" file
$sourceFile = Join-Path $userAppDataLocalDirectory "simpleblazorgrid.db"

Write-Output "Generating test database..."
if (-not (Test-Path -Path $sourceFile -PathType Leaf)) {
    Write-Warning "The 'simpleblazorgrid.db' file does not exist in $userAppDataLocalDirectory."
    exit 1
}

# Duplicate the file as "simpleblazorgrid_test.db" and overwrite if it already exists
$targetFile = Join-Path $userAppDataLocalDirectory "simpleblazorgrid_test.db"
Copy-Item -Path $sourceFile -Destination $targetFile -Force

Write-Output "Done!"
Set-Location -Path $scriptDirectory
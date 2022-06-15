$projects = (

    "src/Lsw.Abp.AntDesignUI",

    "modules/AntDesignTheme/Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme",
	"modules/AntDesignTheme/Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme",
	"modules/AntDesignTheme/Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme",

	"modules/FeatureManagement/Lsw.Abp.FeatureManagement.Blazor.AntDesignUI",
	"modules/FeatureManagement/Lsw.Abp.FeatureManagement.Blazor.Server.AntDesignUI",
	"modules/FeatureManagement/Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI",

	"modules/IdentityManagement/Lsw.Abp.IdentityManagement.Blazor.AntDesignUI",
	"modules/IdentityManagement/Lsw.Abp.IdentityManagement.Blazor.Server.AntDesignUI",
	"modules/IdentityManagement/Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI",

	"modules/PermissionManagement/Lsw.Abp.PermissionManagement.Blazor.AntDesignUI",
	"modules/PermissionManagement/Lsw.Abp.PermissionManagement.Blazor.Server.AntDesignUI",
	"modules/PermissionManagement/Lsw.Abp.PermissionManagement.Blazor.WebAssembly.AntDesignUI",

	"modules/SettingManagement/Lsw.Abp.SettingManagement.Blazor.AntDesignUI",
	"modules/SettingManagement/Lsw.Abp.SettingManagement.Blazor.Server.AntDesignUI",
	"modules/SettingManagement/Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI",

	"modules/TenantManagement/Lsw.Abp.TenantManagement.Blazor.AntDesignUI",
	"modules/TenantManagement/Lsw.Abp.TenantManagement.Blazor.Server.AntDesignUI",
	"modules/TenantManagement/Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI"
    
)

$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

function Write-Info   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Black -BackgroundColor Green

	try 
	{
	   $host.UI.RawUI.WindowTitle = $text
	}		
	catch 
	{
		#Changing window title is not suppoerted!
	}
}

function Write-Error   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Red -BackgroundColor Black 
}

function Seperator   
{
	Write-Host ("_" * 100)  -ForegroundColor gray 
}

function Get-Current-Version { 
	$commonPropsFilePath = resolve-path "../common.props"
	$commonPropsXmlCurrent = [xml](Get-Content $commonPropsFilePath ) 
	$currentVersion = $commonPropsXmlCurrent.Project.PropertyGroup.Version.Trim()
	return $currentVersion
}

function Get-Current-Branch {
	return git branch --show-current
}	   

function Read-File {
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $filePath
    )
		
	$pathExists = Test-Path -Path $filePath -PathType Leaf
	if ($pathExists)
	{
		return Get-Content $filePath		
	}
	else{
		Write-Error  "$filePath path does not exist!"
	}
}

# Delete old packages
del *.nupkg

# Create all packages
$i = 0
$projectsCount = $projects.length
Write-Info "Running dotnet pack on $projectsCount projects..."

foreach($project in $projects) {
    $i += 1
    $projectFolder = Join-Path $rootFolder $project
	$projectName = ($project -split '/')[-1]
		
	# Create nuget pack
    Write-Info "[$i / $projectsCount] - Packing project: $projectName"
	Set-Location $projectFolder
    dotnet clean
    dotnet pack -c Release

    if (-Not $?) {
        Write-Error "Packaging failed for the project: $projectName" 
        exit $LASTEXITCODE
    }
    
    # Move nuget package
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $projectName + ".*.nupkg")
    Move-Item -Force $projectPackPath $packFolder
	
	Seperator
}

# Go back to the pack folder
Set-Location $packFolder
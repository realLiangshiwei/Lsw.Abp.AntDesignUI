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

$apiKey = $args[0]

# Get the version
[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "common.props")
$version = $commonPropsXml.Project.PropertyGroup.Version


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

function Seperator   
{
	Write-Host ("_" * 100)  -ForegroundColor gray 
}

# Publish all packages
$i = 0
$errorCount = 0
$totalProjectsCount = $projects.length
$nugetUrl = "https://api.nuget.org/v3/index.json"
Set-Location $packFolder

foreach($project in $projects) {
	$i += 1
	$projectFolder = Join-Path $rootFolder $project
	$projectName = ($project -split '/')[-1]
	$nugetPackageName = $projectName + "." + $version + ".nupkg"	
	$nugetPackageExists = Test-Path $nugetPackageName -PathType leaf
 
	Write-Info "[$i / $totalProjectsCount] - Pushing: $nugetPackageName"
	
	if ($nugetPackageExists)
	{
		dotnet nuget push $nugetPackageName --skip-duplicate -s $nugetUrl --api-key "$apiKey"		
		#Write-Host ("Deleting package from local: " + $nugetPackageName)
		#Remove-Item $nugetPackageName -Force
	}
	else
	{
		Write-Host ("********** ERROR PACKAGE NOT FOUND: " + $nugetPackageName) -ForegroundColor red
		$errorCount += 1
		#Exit
	}
	
	Write-Host "--------------------------------------------------------------`r`n"
}

if ($errorCount > 0)
{
	Write-Host ("******* $errorCount error(s) occured *******") -ForegroundColor red
}

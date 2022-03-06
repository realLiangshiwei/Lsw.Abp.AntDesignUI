using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.PermissionManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Lsw.Abp.PermissionManagement.Blazor.WebAssembly.AntDesignUI;

[DependsOn(
    typeof(AbpPermissionManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class AbpPermissionManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
}

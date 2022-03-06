using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;
using Lsw.Abp.PermissionManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;

namespace Lsw.Abp.PermissionManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpPermissionManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule)
)]
public class AbpPermissionManagementBlazorServerAntDesignModule : AbpModule
{
}

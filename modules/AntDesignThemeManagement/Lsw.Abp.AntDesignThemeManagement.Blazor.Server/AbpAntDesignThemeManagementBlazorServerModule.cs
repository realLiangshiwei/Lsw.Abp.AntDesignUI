using Lsw.Abp.AntDesignThemeManagement.Blazor;
using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;
using Volo.Abp.Modularity;

namespace Lsw.Abp.AntDesignThemeManagement.Blazor.Server;

[DependsOn(
    typeof(AbpAntDesignThemeManagementBlazorModule),
    typeof(AbpAntDesignThemeManagementApplicationModule),
    typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule)
)]
public class AbpAntDesignThemeManagementBlazorServerModule : AbpModule
{
}

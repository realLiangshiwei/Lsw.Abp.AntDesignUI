using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;
using Volo.Abp.Modularity;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI;

namespace Lsw.Abp.SettingManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule)
)]
public class AbpSettingManagementBlazorServerAntDesignModule : AbpModule
{
    
}

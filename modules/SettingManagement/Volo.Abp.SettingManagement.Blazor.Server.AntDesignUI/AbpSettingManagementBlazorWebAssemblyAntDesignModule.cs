using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.AntDesignUI;

namespace Volo.Abp.SettingManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule)
)]
public class AbpSettingManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
    
}

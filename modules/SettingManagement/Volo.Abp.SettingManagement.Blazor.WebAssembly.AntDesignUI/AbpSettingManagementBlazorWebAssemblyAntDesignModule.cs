using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.AntDesignUI;

namespace Volo.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AbpSettingManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
    
}

using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Volo.Abp.Modularity;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI;
using Volo.Abp.SettingManagement;

namespace Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AbpSettingManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
    
}

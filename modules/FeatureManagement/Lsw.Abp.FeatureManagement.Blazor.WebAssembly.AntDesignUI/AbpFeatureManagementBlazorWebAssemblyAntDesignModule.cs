using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.FeatureManagement.Blazor.AntDesignUI;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;

namespace Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI;

[DependsOn(
    typeof(AbpFeatureManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpFeatureManagementHttpApiClientModule)
)]
public class AbpFeatureManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
}

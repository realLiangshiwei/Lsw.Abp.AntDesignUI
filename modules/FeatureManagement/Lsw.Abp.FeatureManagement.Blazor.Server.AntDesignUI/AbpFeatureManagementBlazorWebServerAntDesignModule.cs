using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;
using Lsw.Abp.FeatureManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;

namespace Lsw.Abp.FeatureManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpFeatureManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule)
)]
public class AbpFeatureManagementBlazorWebServerAntDesignModule : AbpModule
{
}

using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Modularity;

namespace Lsw.Abp.FeatureManagement.Blazor.AntDesignUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpFeaturesModule)
)]
public class AbpFeatureManagementBlazorAntDesignModule : AbpModule
{

}

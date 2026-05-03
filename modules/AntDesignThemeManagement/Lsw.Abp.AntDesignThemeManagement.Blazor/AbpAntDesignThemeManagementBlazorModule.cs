using Lsw.Abp.AntDesignThemeManagement.Blazor.Settings;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor;

namespace Lsw.Abp.AntDesignThemeManagement.Blazor;

[DependsOn(
    typeof(AbpAntDesignThemeManagementApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule),
    typeof(AbpSettingManagementBlazorAntDesignModule)
)]
public class AbpAntDesignThemeManagementBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new AntDesignThemeManagementSettingContributor());
        });
    }
}

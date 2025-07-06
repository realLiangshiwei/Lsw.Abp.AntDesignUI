using Lsw.Abp.AntDesignUI;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;

[DependsOn(
    typeof(AbpAntDesignUIModule),
    typeof(AbpUiNavigationModule)
)]
public class AbpAspNetCoreComponentsWebAntDesignThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDynamicLayoutComponentOptions>(options =>
        {
            options.Components.Add(typeof(AbpAuthenticationState), null);
        });
    }
}

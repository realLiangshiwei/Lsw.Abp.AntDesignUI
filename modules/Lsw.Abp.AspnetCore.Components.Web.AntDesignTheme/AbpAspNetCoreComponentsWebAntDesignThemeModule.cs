using Lsw.Abp.AntDesignUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;

[DependsOn(
    typeof(AbpAntDesignUIModule),
    typeof(AbpUiNavigationModule)
)]
public class AbpAspNetCoreComponentsWebAntDesignThemeModule : AbpModule
{
    
}

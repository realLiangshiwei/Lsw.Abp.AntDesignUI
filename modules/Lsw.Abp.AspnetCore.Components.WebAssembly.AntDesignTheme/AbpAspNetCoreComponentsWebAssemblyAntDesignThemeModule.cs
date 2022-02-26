using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Modularity;

namespace Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyModule)
)]
public class AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule : AbpModule
{
    
}

using Lsw.Abp.AntDesignThemeManagement.Blazor;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Lsw.Abp.AntDesignThemeManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpAntDesignThemeManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpHttpClientIdentityModelWebAssemblyModule)
)]
public class AbpAntDesignThemeManagementBlazorWebAssemblyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpAntDesignThemeManagementApplicationContractsModule).Assembly);
    }
}

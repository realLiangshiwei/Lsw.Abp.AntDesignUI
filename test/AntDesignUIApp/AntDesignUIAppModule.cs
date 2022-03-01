using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace AntDesignUIApp;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule))]
public class AntDesignUIAppModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();
        
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(AntDesignUIAppModule).Assembly;
        });
        
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new AntDesignUiAppMenuContributor());
        });
        
        
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });

        context.Services.AddAuthorizationCore();
        context.Services.AddAlwaysAllowAuthorization();
        context.Services.AddScoped<AuthenticationStateProvider, FakeAuthStateProvider>();
        
        builder.RootComponents.Add<AppWithoutAuth>("#ApplicationContainer");

        context.Services.RemoveAll(x => x.ImplementationType == typeof(AbpApplicationConfigurationClientProxy));
        
        context.Services.AddTransient<AbpApplicationConfigurationClientProxy, FakeAbpApplicationConfigurationAppService>();
        

    }
}

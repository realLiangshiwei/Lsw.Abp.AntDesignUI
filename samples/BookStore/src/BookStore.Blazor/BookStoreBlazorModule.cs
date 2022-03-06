using System;
using System.Net.Http;
using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Blazor.Menus;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;
using Volo.Abp.UI.Navigation;

namespace BookStore.Blazor;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(BookStoreHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpIdentityBlazorWebAssemblyAntDesignModule),
    typeof(AbpTenantManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpSettingManagementBlazorWebAssemblyAntDesignModule)
)]
public class BookStoreBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureRouter(context);
        ConfigureUI(builder);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options => { options.AppAssembly = typeof(BookStoreBlazorModule).Assembly; });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BookStoreMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.UserOptions.RoleClaim = JwtClaimTypes.Role;
            options.ProviderOptions.DefaultScopes.Add("BookStore");
            options.ProviderOptions.DefaultScopes.Add("role");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("phone");
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#ApplicationContainer");
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context,
        IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<BookStoreBlazorModule>(); });
    }
}

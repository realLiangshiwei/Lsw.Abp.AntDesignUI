using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BookStoreWebApp.Blazor.Client.Menus;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace BookStoreWebApp.Blazor.Client;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(BookStoreWebAppHttpApiClientModule),
    // typeof(AbpAspNetCoreComponentsWebAssemblyLeptonXLiteThemeModule),
    // typeof(AbpIdentityBlazorWebAssemblyModule),
    // typeof(AbpTenantManagementBlazorWebAssemblyModule),
    // typeof(AbpSettingManagementBlazorWebAssemblyModule)
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpIdentityBlazorWebAssemblyAntDesignModule),
    typeof(AbpTenantManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpSettingManagementBlazorWebAssemblyAntDesignModule)
)]
public class BookStoreWebAppBlazorClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpAspNetCoreComponentsWebOptions>(options =>
        {
            options.IsBlazorWebApp = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        //ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(BookStoreWebAppBlazorClientModule).Assembly;
            options.AdditionalAssemblies.Add(typeof(BookStoreWebAppBlazorClientModule).Assembly);
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BookStoreWebAppMenuContributor(context.Services.GetConfiguration()));
        });
    }

    // private void ConfigureBlazorise(ServiceConfigurationContext context)
    // {
    //     context.Services
    //         .AddBootstrap5Providers()
    //         .AddFontAwesomeIcons();
    // }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        //TODO: Remove SignOutSessionStateManager in new version.
        builder.Services.TryAddScoped<SignOutSessionStateManager>();
        builder.Services.AddBlazorWebAppServices();
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BookStoreWebAppBlazorClientModule>();
        });
    }
}

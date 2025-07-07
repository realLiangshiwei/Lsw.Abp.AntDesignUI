using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BookStore.Blazor.Client.Navigation;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using OpenIddict.Abstractions;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI;


namespace BookStore.Blazor.Client;

[DependsOn(
    // typeof(AbpSettingManagementBlazorWebAssemblyModule),
    // typeof(AbpFeatureManagementBlazorWebAssemblyModule),
    // typeof(AbpIdentityBlazorWebAssemblyModule),
    // typeof(AbpTenantManagementBlazorWebAssemblyModule),
    // typeof(AbpAspNetCoreComponentsWebAssemblyLeptonXLiteThemeModule),
    typeof(AbpSettingManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpIdentityBlazorWebAssemblyAntDesignModule),
    typeof(AbpTenantManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(AbpAutofacWebAssemblyModule),
    typeof(BookStoreHttpApiClientModule)
)]
public class BookStoreBlazorClientModule : AbpModule
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
            options.AppAssembly = typeof(BookStoreBlazorClientModule).Assembly;
            options.AdditionalAssemblies.Add(typeof(BookStoreBlazorClientModule).Assembly);
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BookStoreMenuContributor(context.Services.GetConfiguration()));
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
            options.AddMaps<BookStoreBlazorClientModule>();
        });
    }
}

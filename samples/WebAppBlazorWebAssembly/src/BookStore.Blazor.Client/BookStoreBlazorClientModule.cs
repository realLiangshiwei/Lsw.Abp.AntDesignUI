﻿using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Blazor.Client.Navigation;
using Localization.Resources.AbpUi;
using Volo.Abp.Localization;
using BookStore.Localization;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;
using Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI;
using OpenIddict.Abstractions;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace BookStore.Blazor.Client;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(AbpIdentityBlazorWebAssemblyAntDesignModule),
    typeof(AbpSettingManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpTenantManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
    typeof(BookStoreHttpApiClientModule)
)]
public class BookStoreBlazorClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureLocalization();
        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        //ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }
    
    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BookStoreResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(BookStoreBlazorClientModule).Assembly;
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
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.UserOptions.NameClaim = OpenIddictConstants.Claims.Name;
            options.UserOptions.RoleClaim = OpenIddictConstants.Claims.Role;

            options.ProviderOptions.DefaultScopes.Add("BookStore");
            options.ProviderOptions.DefaultScopes.Add("roles");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("phone");
        });
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

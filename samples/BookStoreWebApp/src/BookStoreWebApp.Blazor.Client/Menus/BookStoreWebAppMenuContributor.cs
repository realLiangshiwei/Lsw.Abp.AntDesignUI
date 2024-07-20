using System;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.Extensions.Configuration;
using BookStoreWebApp.Localization;
using BookStoreWebApp.MultiTenancy;
using Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.AntDesignUI;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace BookStoreWebApp.Blazor.Client.Menus;

public class BookStoreWebAppMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public BookStoreWebAppMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookStoreWebAppResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BookStoreWebAppMenus.Home,
                l["Menu:Home"],
                "/",
                icon: IconType.Outline.Home
            )
        );

        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        if (!OperatingSystem.IsBrowser())
        {
            return Task.CompletedTask;
        }

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        context.Menu.AddItem(new ApplicationMenuItem(
                "Account.Manage",
                accountStringLocalizer["MyAccount"],
                $"{authServerUrl.EnsureEndsWith('/')}Account/Manage",
                icon: IconType.Outline.Setting,
                order: 1000,
                target: "_blank")
            .RequireAuthenticated());

        return Task.CompletedTask;
    }
}

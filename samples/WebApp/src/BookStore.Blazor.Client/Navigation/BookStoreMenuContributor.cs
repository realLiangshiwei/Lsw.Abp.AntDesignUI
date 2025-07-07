using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BookStore.Localization;
using BookStore.Permissions;
using BookStore.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Localization.Resources.AbpUi;
using Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.AntDesignUI;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;

namespace BookStore.Blazor.Client.Navigation;

public class BookStoreMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public BookStoreMenuContributor(IConfiguration configuration)
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

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookStoreResource>();
        
        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        context.Menu.AddItem(new ApplicationMenuItem(
            BookStoreMenus.Home,
            l["Menu:Home"],
            "/",
            icon: "IconType.Outline.Home",
            order: 1
        ));
        
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
    }
    
    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        if (OperatingSystem.IsBrowser())
        {
            //Blazor wasm menu items

            var authServerUrl = _configuration["AuthServer:Authority"] ?? "";
            var accountResource = context.GetLocalizer<AccountResource>();

            context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], $"{authServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "IconType.Outline.Setting", order: 900,  target: "_blank").RequireAuthenticated());

        }
        else
        {
            //Blazor server menu items

        }
        await Task.CompletedTask;
    }
}

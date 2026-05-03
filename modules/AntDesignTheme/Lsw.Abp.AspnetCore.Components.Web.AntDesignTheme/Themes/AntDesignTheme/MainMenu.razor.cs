using System;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class MainMenu : AbpComponentBase, IDisposable
{
    protected ApplicationMenu Menu { get; set; } = default!;

    [Inject]
    protected IMenuManager MenuManager { get; set; } = default!;

    [Inject]
    protected ApplicationConfigurationChangedService ApplicationConfigurationChangedService { get; set; } = default!;

    [Parameter]
    public MenuPlacement Placement { get; set; }

    [Parameter]
    public MenuTheme Theme { get; set; }

    [Parameter]
    public bool Collapsed { get; set; }

    [Parameter]
    public EventCallback OnMenuItemClick { get; set; }

    protected string MenuRenderKey { get; set; } = $"lsw-main-menu-{Guid.NewGuid():N}";

    protected MenuMode MenuMode => Placement == MenuPlacement.Left ? MenuMode.Inline : MenuMode.Horizontal;

    protected bool InlineCollapsedValue => Placement == MenuPlacement.Left && Collapsed;

    protected Trigger TriggerSubMenuAction => Placement == MenuPlacement.Top ? Trigger.Click : Trigger.Hover;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await GetMenuAsync();
            ApplicationConfigurationChangedService.Changed += ApplicationConfigurationChanged;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task GetMenuAsync()
    {
        Menu = await MenuManager.GetMainMenuAsync();
    }

    private async void ApplicationConfigurationChanged()
    {
        try
        {
            await GetMenuAsync();
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task OnMenuItemClickedAsync()
    {
        if (Placement == MenuPlacement.Top)
        {
            // Force-close any opened top-mode dropdowns once an item is clicked.
            MenuRenderKey = $"lsw-main-menu-{Guid.NewGuid():N}";
        }

        if (OnMenuItemClick.HasDelegate)
        {
            await OnMenuItemClick.InvokeAsync();
        }
    }

    public void Dispose()
    {
        ApplicationConfigurationChangedService.Changed -= ApplicationConfigurationChanged;
    }
}

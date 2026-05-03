using System;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Lsw.Abp.AntDesignThemeManagement.Settings;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class DefaultLayout : IDisposable
{
    protected const int MobileCollapsedSiderWidth = 64;

    [Inject]
    protected IAntDesignSettingsProvider AntDesignSettingsProvider { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    protected bool Collapsed { get; set; }

    protected MenuPlacement MenuPlacement { get; set; }

    protected MenuTheme MenuTheme { get; set; }

    protected AntDesignThemePreferenceDto Preference { get; set; } = new();

    protected string HeaderClass { get; set; } = string.Empty;

    protected SiderTheme SiderTheme { get; set; }

    protected string SiderStyle { get; set; } = "min-width:256px;max-width:256px;width:256px;";

    protected string SiderClass => IsMobile ? "ant-design-side lsw-pro-side-mobile" : "ant-design-side";

    protected string LayoutClass { get; set; } = "ant-design-layout lsw-pro-theme lsw-pro-theme-light";

    protected string ThemeBodyClass { get; set; } = "lsw-pro-theme lsw-pro-theme-light";

    protected bool IsMobile { get; set; }

    protected bool IsTopNavigation => MenuPlacement == MenuPlacement.Top;

    protected bool ShowHeader => Preference.ShowHeader;

    protected bool ShowFooter => Preference.ShowFooter;

    protected bool ShowMenu => Preference.ShowMenu;

    protected bool ShowMenuHeader => Preference.ShowMenu && Preference.ShowMenuHeader;

    protected bool IsFixedContentWidth => Preference.ContentWidth == ContentWidths.Fixed;

    protected override async Task OnInitializedAsync()
    {
        await SetLayoutAsync();
        AntDesignSettingsProvider.SettingChanged += OnSettingChangedAsync;
    }

    protected virtual async Task OnSettingChangedAsync()
    {
        await SetLayoutAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("lswAntDesignThemeSettings.applyThemeClass", ThemeBodyClass);
        }
        catch
        {
            // Ignore transient script loading errors.
        }
    }

    protected virtual async Task SetLayoutAsync()
    {
        Preference = await AntDesignSettingsProvider.GetPreferenceAsync();

        MenuPlacement = Preference.NavigationMode == NavigationModes.Top
            ? MenuPlacement.Top
            : MenuPlacement.Left;

        MenuTheme = Preference.ThemeStyle == ThemeStyles.Light
            ? MenuTheme.Light
            : MenuTheme.Dark;

        SiderTheme = MenuTheme == MenuTheme.Light ? SiderTheme.Light : SiderTheme.Dark;

        var headerBaseClass = IsTopNavigation ? "ant-design-header-top" : "ant-design-header-left";
        var headerModeClass = MenuTheme == MenuTheme.Light ? $"{headerBaseClass}-light" : $"{headerBaseClass}-dark";
        var fixedClass = Preference.FixedHeader ? "ant-design-header-fixed" : string.Empty;
        HeaderClass = $"{headerBaseClass} {headerModeClass} {fixedClass}".Trim();

        LayoutClass = BuildLayoutClass();
        ThemeBodyClass = BuildThemeBodyClass();
        SiderStyle = BuildSiderStyle();
    }

    protected virtual void OnCollapse()
    {
        Collapsed = !Collapsed;
        SiderStyle = BuildSiderStyle();
    }

    protected virtual Task OnSiderBreakpointChanged(bool broken)
    {
        IsMobile = broken;

        if (broken)
        {
            Collapsed = true;
        }
        else if (Collapsed)
        {
            Collapsed = false;
        }

        SiderStyle = BuildSiderStyle();
        return InvokeAsync(StateHasChanged);
    }

    protected virtual void CloseMobileMenu()
    {
        if (!IsMobile)
        {
            return;
        }

        Collapsed = true;
        SiderStyle = BuildSiderStyle();
    }

    protected virtual Task OnMainMenuItemClickAsync()
    {
        if (IsMobile && !Collapsed)
        {
            Collapsed = true;
            SiderStyle = BuildSiderStyle();
            return InvokeAsync(StateHasChanged);
        }

        return Task.CompletedTask;
    }

    protected virtual string BuildLayoutClass()
    {
        var themeClass = GetThemeClass();
        var weakClass = Preference.ColorWeak ? "colorWeak" : string.Empty;
        var fixedContentClass = IsFixedContentWidth ? "lsw-pro-content-fixed" : "lsw-pro-content-fluid";
        var navModeClass = IsTopNavigation ? "lsw-pro-nav-top" : "lsw-pro-nav-side";

        return $"ant-design-layout lsw-pro-theme {themeClass} {weakClass} {fixedContentClass} {navModeClass}".Trim();
    }

    protected virtual string BuildThemeBodyClass()
    {
        var themeClass = GetThemeClass();
        var weakClass = Preference.ColorWeak ? "colorWeak" : string.Empty;

        return $"lsw-pro-theme {themeClass} {weakClass}".Trim();
    }

    protected virtual string GetThemeClass()
    {
        return Preference.ThemeStyle switch
        {
            ThemeStyles.Dark => "lsw-pro-theme-dark",
            ThemeStyles.RealDark => "lsw-pro-theme-real-dark",
            _ => "lsw-pro-theme-light"
        };
    }

    protected virtual string BuildSiderStyle()
    {
        var width = Collapsed
            ? (IsMobile ? MobileCollapsedSiderWidth : 80)
            : 256;

        var mobileSiderStyle = IsMobile
            ? "position:fixed;left:0;top:0;height:100vh;overflow:auto;z-index:1310;"
            : string.Empty;

        var fixedSiderStyle = Preference.FixSiderbar
            && !IsMobile
            ? "position:sticky;top:0;height:100vh;overflow:auto;"
            : string.Empty;

        return $"min-width:{width}px;max-width:{width}px;width:{width}px;{mobileSiderStyle}{fixedSiderStyle}";
    }

    public void Dispose()
    {
        AntDesignSettingsProvider.SettingChanged -= OnSettingChangedAsync;
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Lsw.Abp.AntDesignThemeManagement.Localization;
using Lsw.Abp.AntDesignThemeManagement.Settings;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Volo.Abp.Users;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class ThemeSettingPanel : IAsyncDisposable
{
    [Inject]
    protected IStringLocalizer<AntDesignThemeManagementResource> ThemeL { get; set; } = default!;

    [Inject]
    protected IAntDesignSettingsProvider AntDesignSettingsProvider { get; set; } = default!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = default!;

    protected bool PanelVisible { get; set; }

    protected bool IsVisible { get; set; }

    protected AntDesignThemePreferenceDto Preference { get; set; } = new();

    protected string HostId { get; } = $"lsw-theme-settings-fab-{Guid.NewGuid():N}";

    protected string FloatingButtonClass =>
        $"lsw-theme-setting-fab {(PanelVisible ? "lsw-theme-setting-fab-active" : string.Empty)}".Trim();

    private bool _isSubscribedToSettingChanged;
    private bool _jsInitialized;

    protected IReadOnlyList<OptionItem> ThemeStyleOptions { get; } = new[]
    {
        new OptionItem(ThemeStyles.Light, "Light", "lsw-theme-preview-light"),
        new OptionItem(ThemeStyles.Dark, "Dark", "lsw-theme-preview-dark"),
        new OptionItem(ThemeStyles.RealDark, "RealDark", "lsw-theme-preview-real-dark")
    };

    protected IReadOnlyList<OptionItem> NavigationOptions { get; } = new[]
    {
        new OptionItem(NavigationModes.Side, "Side", "lsw-theme-preview-nav-side"),
        new OptionItem(NavigationModes.Top, "Top", "lsw-theme-preview-nav-top")
    };

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!CurrentUser.IsAuthenticated)
            {
                IsVisible = false;
                return;
            }

            AntDesignSettingsProvider.SettingChanged += OnSettingChangedAsync;
            _isSubscribedToSettingChanged = true;

            Preference = await AntDesignSettingsProvider.GetPreferenceAsync();
            IsVisible = Preference.ThemeSettingsEnabled;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
            IsVisible = false;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!IsVisible || _jsInitialized)
        {
            return;
        }

        try
        {
            await JsRuntime.InvokeVoidAsync("lswAntDesignThemeSettings.initialize", HostId);
            _jsInitialized = true;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task TogglePanel()
    {
        PanelVisible = !PanelVisible;
        return Task.CompletedTask;
    }

    protected virtual Task OnFabKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key is "Enter" or " ")
        {
            return TogglePanel();
        }

        return Task.CompletedTask;
    }

    protected virtual Task ClosePanel()
    {
        PanelVisible = false;
        return Task.CompletedTask;
    }

    protected virtual async Task SetThemeStyleAsync(string style)
    {
        Preference.ThemeStyle = style;
        await SaveAsync();
    }

    protected virtual async Task SetNavigationModeAsync(string mode)
    {
        var previousMode = Preference.NavigationMode;
        Preference.NavigationMode = mode;
        await SaveAsync();

        if (Preference.NavigationMode != mode)
        {
            Preference.NavigationMode = previousMode;
        }
    }

    protected virtual async Task OnContentWidthChangedAsync(ChangeEventArgs args)
    {
        Preference.ContentWidth = args.Value?.ToString() ?? ContentWidths.Fluid;
        await SaveAsync();
    }

    protected virtual async Task OnFixedHeaderChangedAsync(ChangeEventArgs args)
    {
        Preference.FixedHeader = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnFixedSiderbarChangedAsync(ChangeEventArgs args)
    {
        Preference.FixSiderbar = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnSplitMenusChangedAsync(ChangeEventArgs args)
    {
        Preference.SplitMenus = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnShowHeaderChangedAsync(ChangeEventArgs args)
    {
        Preference.ShowHeader = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnShowFooterChangedAsync(ChangeEventArgs args)
    {
        Preference.ShowFooter = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnShowMenuChangedAsync(ChangeEventArgs args)
    {
        Preference.ShowMenu = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnShowMenuHeaderChangedAsync(ChangeEventArgs args)
    {
        Preference.ShowMenuHeader = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual async Task OnColorWeakChangedAsync(ChangeEventArgs args)
    {
        Preference.ColorWeak = ReadBoolean(args);
        await SaveAsync();
    }

    protected virtual bool ReadBoolean(ChangeEventArgs args)
    {
        return args.Value switch
        {
            bool boolValue => boolValue,
            string stringValue when bool.TryParse(stringValue, out var result) => result,
            _ => false
        };
    }

    protected virtual async Task SaveAsync()
    {
        try
        {
            await AntDesignSettingsProvider.ApplyPreferenceAsync(new UpdateAntDesignThemePreferenceDto
            {
                ThemeStyle = Preference.ThemeStyle,
                NavigationMode = Preference.NavigationMode,
                ContentWidth = Preference.ContentWidth,
                FixedHeader = Preference.FixedHeader,
                FixSiderbar = Preference.FixSiderbar,
                SplitMenus = Preference.SplitMenus,
                ShowHeader = Preference.ShowHeader,
                ShowFooter = Preference.ShowFooter,
                ShowMenu = Preference.ShowMenu,
                ShowMenuHeader = Preference.ShowMenuHeader,
                ColorWeak = Preference.ColorWeak
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
            try
            {
                Preference = await AntDesignSettingsProvider.GetPreferenceAsync();
            }
            catch
            {
                // Intentionally ignored to keep the panel responsive even if reloading fails.
            }

            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual async Task OnSettingChangedAsync()
    {
        try
        {
            if (!CurrentUser.IsAuthenticated)
            {
                return;
            }

            Preference = await AntDesignSettingsProvider.GetPreferenceAsync();
            IsVisible = Preference.ThemeSettingsEnabled;
            if (!IsVisible)
            {
                PanelVisible = false;
            }
            else
            {
                _jsInitialized = false;
            }

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_isSubscribedToSettingChanged)
        {
            AntDesignSettingsProvider.SettingChanged -= OnSettingChangedAsync;
            _isSubscribedToSettingChanged = false;
        }

        try
        {
            await JsRuntime.InvokeVoidAsync("lswAntDesignThemeSettings.dispose", HostId);
        }
        catch
        {
            // Ignore disposal errors when the JS runtime is no longer available.
        }
    }

    protected record OptionItem(string Value, string LocalizationKey, string CssClass);
}

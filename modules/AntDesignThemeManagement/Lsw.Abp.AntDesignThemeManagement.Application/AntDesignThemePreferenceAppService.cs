using System;
using System.Threading.Tasks;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Lsw.Abp.AntDesignThemeManagement.Permissions;
using Lsw.Abp.AntDesignThemeManagement.Settings;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Lsw.Abp.AntDesignThemeManagement;

public class AntDesignThemePreferenceAppService : ApplicationService, IAntDesignThemePreferenceAppService
{
    protected ISettingProvider AbpSettingProvider { get; }
    protected ISettingManager AbpSettingManager { get; }
    protected IPermissionChecker PermissionChecker { get; }

    public AntDesignThemePreferenceAppService(
        ISettingProvider settingProvider,
        ISettingManager settingManager,
        IPermissionChecker permissionChecker)
    {
        AbpSettingProvider = settingProvider;
        AbpSettingManager = settingManager;
        PermissionChecker = permissionChecker;
    }

    public virtual async Task<AntDesignThemePreferenceDto> GetAsync()
    {
        var preference = new AntDesignThemePreferenceDto
        {
            ThemeSettingsEnabled = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.EnableThemeSettings,
                AntDesignThemeSettingDefaults.EnableThemeSettings),
            PageStyleSettingEnabled = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.EnablePageStyleSetting,
                AntDesignThemeSettingDefaults.EnablePageStyleSetting),
            NavigationModeSettingEnabled = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.EnableNavigationModeSetting,
                AntDesignThemeSettingDefaults.EnableNavigationModeSetting),
            RegionalSettingsEnabled = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.EnableRegionalSettings,
                AntDesignThemeSettingDefaults.EnableRegionalSettings),
            OtherSettingsEnabled = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.EnableOtherSettings,
                AntDesignThemeSettingDefaults.EnableOtherSettings),
            ThemeStyle = await GetThemeStyleAsync(),
            NavigationMode = await GetNavigationModeAsync(),
            ContentWidth = await GetContentWidthAsync(),
            FixedHeader = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.FixedHeader,
                AntDesignThemeSettingDefaults.FixedHeader),
            FixSiderbar = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.FixSiderbar,
                AntDesignThemeSettingDefaults.FixSiderbar),
            SplitMenus = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.SplitMenus,
                AntDesignThemeSettingDefaults.SplitMenus),
            ShowHeader = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.ShowHeader,
                AntDesignThemeSettingDefaults.ShowHeader),
            ShowFooter = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.ShowFooter,
                AntDesignThemeSettingDefaults.ShowFooter),
            ShowMenu = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.ShowMenu,
                AntDesignThemeSettingDefaults.ShowMenu),
            ShowMenuHeader = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.ShowMenuHeader,
                AntDesignThemeSettingDefaults.ShowMenuHeader),
            ColorWeak = await GetBoolAsync(
                AntDesignThemeManagementSettingNames.ColorWeak,
                AntDesignThemeSettingDefaults.ColorWeak)
        };

        NormalizeThemeSettingsAvailability(preference);
        return preference;
    }

    public virtual async Task UpdateAsync(UpdateAntDesignThemePreferenceDto input)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            throw new AbpAuthorizationException("Current user must be authenticated.");
        }

        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ThemeStyle,
            NormalizeThemeStyle(input.ThemeStyle));
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.NavigationMode,
            NormalizeNavigationMode(input.NavigationMode));
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ContentWidth,
            NormalizeContentWidth(input.ContentWidth));
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.FixedHeader,
            input.FixedHeader.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.FixSiderbar,
            input.FixSiderbar.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.SplitMenus,
            input.SplitMenus.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ShowHeader,
            input.ShowHeader.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ShowFooter,
            input.ShowFooter.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ShowMenu,
            input.ShowMenu.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ShowMenuHeader,
            input.ShowMenuHeader.ToString().ToLowerInvariant());
        await AbpSettingManager.SetForCurrentUserAsync(
            AntDesignThemeManagementSettingNames.ColorWeak,
            input.ColorWeak.ToString().ToLowerInvariant());
    }

    public virtual async Task UpdateThemeSettingsAvailabilityAsync(UpdateAntDesignThemeSettingsAvailabilityDto input)
    {
        if (!await PermissionChecker.IsGrantedAsync(AntDesignThemeManagementPermissions.Settings))
        {
            throw new AbpAuthorizationException("Missing permission to update global theme settings.");
        }

        var normalized = NormalizeThemeSettingsAvailability(input);
        await SetThemeSettingsAvailabilityAsync(normalized);
    }

    public virtual async Task SetThemeSettingsEnabledAsync(bool isEnabled)
    {
        if (!await PermissionChecker.IsGrantedAsync(AntDesignThemeManagementPermissions.Settings))
        {
            throw new AbpAuthorizationException("Missing permission to update global theme settings.");
        }

        var normalized = isEnabled
            ? new ThemeSettingsAvailability(true, true, true, true, true)
            : new ThemeSettingsAvailability(false, false, false, false, false);

        await SetThemeSettingsAvailabilityAsync(normalized);
    }

    protected virtual async Task<string> GetThemeStyleAsync()
    {
        var value = await GetStringAsync(
            AntDesignThemeManagementSettingNames.ThemeStyle,
            AntDesignThemeSettingDefaults.ThemeStyle);

        return NormalizeThemeStyle(value);
    }

    protected virtual async Task<string> GetNavigationModeAsync()
    {
        var value = await GetStringAsync(
            AntDesignThemeManagementSettingNames.NavigationMode,
            AntDesignThemeSettingDefaults.NavigationMode);

        return NormalizeNavigationMode(value);
    }

    protected virtual async Task<string> GetContentWidthAsync()
    {
        var value = await GetStringAsync(
            AntDesignThemeManagementSettingNames.ContentWidth,
            AntDesignThemeSettingDefaults.ContentWidth);

        return NormalizeContentWidth(value);
    }

    protected virtual async Task<string> GetStringAsync(string name, string defaultValue)
    {
        var value = await AbpSettingProvider.GetOrNullAsync(name);
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    protected virtual async Task<bool> GetBoolAsync(string name, bool defaultValue)
    {
        var value = await AbpSettingProvider.GetOrNullAsync(name);
        return bool.TryParse(value, out var parsed) ? parsed : defaultValue;
    }

    protected virtual void NormalizeThemeSettingsAvailability(AntDesignThemePreferenceDto preference)
    {
        var hasAnyEnabledSection = preference.PageStyleSettingEnabled
            || preference.NavigationModeSettingEnabled
            || preference.RegionalSettingsEnabled
            || preference.OtherSettingsEnabled;

        preference.ThemeSettingsEnabled = hasAnyEnabledSection;
    }

    protected virtual ThemeSettingsAvailability NormalizeThemeSettingsAvailability(
        UpdateAntDesignThemeSettingsAvailabilityDto input)
    {
        if (!input.ThemeSettingsEnabled)
        {
            return new ThemeSettingsAvailability(false, false, false, false, false);
        }

        var hasAnyEnabledSection = input.PageStyleSettingEnabled
            || input.NavigationModeSettingEnabled
            || input.RegionalSettingsEnabled
            || input.OtherSettingsEnabled;

        if (!hasAnyEnabledSection)
        {
            // Enabling the root switch should turn on all sub-items by default.
            return new ThemeSettingsAvailability(true, true, true, true, true);
        }

        return new ThemeSettingsAvailability(
            true,
            input.PageStyleSettingEnabled,
            input.NavigationModeSettingEnabled,
            input.RegionalSettingsEnabled,
            input.OtherSettingsEnabled
        );
    }

    protected virtual async Task SetThemeSettingsAvailabilityAsync(ThemeSettingsAvailability availability)
    {
        await AbpSettingManager.SetGlobalAsync(
            AntDesignThemeManagementSettingNames.EnableThemeSettings,
            availability.ThemeSettingsEnabled.ToString().ToLowerInvariant());
        await AbpSettingManager.SetGlobalAsync(
            AntDesignThemeManagementSettingNames.EnablePageStyleSetting,
            availability.PageStyleSettingEnabled.ToString().ToLowerInvariant());
        await AbpSettingManager.SetGlobalAsync(
            AntDesignThemeManagementSettingNames.EnableNavigationModeSetting,
            availability.NavigationModeSettingEnabled.ToString().ToLowerInvariant());
        await AbpSettingManager.SetGlobalAsync(
            AntDesignThemeManagementSettingNames.EnableRegionalSettings,
            availability.RegionalSettingsEnabled.ToString().ToLowerInvariant());
        await AbpSettingManager.SetGlobalAsync(
            AntDesignThemeManagementSettingNames.EnableOtherSettings,
            availability.OtherSettingsEnabled.ToString().ToLowerInvariant());
    }

    protected virtual string NormalizeThemeStyle(string themeStyle)
    {
        return themeStyle switch
        {
            ThemeStyles.Light => ThemeStyles.Light,
            ThemeStyles.Dark => ThemeStyles.Dark,
            ThemeStyles.RealDark => ThemeStyles.RealDark,
            _ => AntDesignThemeSettingDefaults.ThemeStyle
        };
    }

    protected virtual string NormalizeNavigationMode(string navigationMode)
    {
        return navigationMode switch
        {
            NavigationModes.Side => NavigationModes.Side,
            NavigationModes.Top => NavigationModes.Top,
            NavigationModes.Mix => NavigationModes.Side,
            _ => AntDesignThemeSettingDefaults.NavigationMode
        };
    }

    protected virtual string NormalizeContentWidth(string contentWidth)
    {
        return contentWidth switch
        {
            ContentWidths.Fluid => ContentWidths.Fluid,
            ContentWidths.Fixed => ContentWidths.Fixed,
            _ => AntDesignThemeSettingDefaults.ContentWidth
        };
    }

    protected record ThemeSettingsAvailability(
        bool ThemeSettingsEnabled,
        bool PageStyleSettingEnabled,
        bool NavigationModeSettingEnabled,
        bool RegionalSettingsEnabled,
        bool OtherSettingsEnabled
    );
}

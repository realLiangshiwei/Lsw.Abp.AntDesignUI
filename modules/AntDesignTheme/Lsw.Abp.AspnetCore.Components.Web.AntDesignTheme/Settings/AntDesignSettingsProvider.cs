using System;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignThemeManagement;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Lsw.Abp.AntDesignThemeManagement.Settings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;

public class AntDesignSettingsProvider : IAntDesignSettingsProvider, IScopedDependency
{
    protected IAntDesignThemePreferenceAppService ThemePreferenceAppService { get; }

    protected ICurrentUser CurrentUser { get; }

    public AntDesignSettingsProvider(
        IAntDesignThemePreferenceAppService themePreferenceAppService,
        ICurrentUser currentUser)
    {
        ThemePreferenceAppService = themePreferenceAppService;
        CurrentUser = currentUser;
    }

    public event Func<Task>? SettingChanged;

    public async Task<AntDesignThemePreferenceDto> GetPreferenceAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return BuildDefaultPreference();
        }

        try
        {
            return await ThemePreferenceAppService.GetAsync();
        }
        catch
        {
            return BuildDefaultPreference();
        }
    }

    public async Task<MenuPlacement> GetMenuPlacementAsync()
    {
        var setting = await GetPreferenceAsync();

        return setting.NavigationMode == NavigationModes.Top
            ? MenuPlacement.Top
            : MenuPlacement.Left;
    }

    public async Task<MenuTheme> GetMenuThemeAsync()
    {
        var setting = await GetPreferenceAsync();

        return setting.ThemeStyle == ThemeStyles.Light
            ? MenuTheme.Light
            : MenuTheme.Dark;
    }

    public async Task ApplyPreferenceAsync(UpdateAntDesignThemePreferenceDto input)
    {
        await ThemePreferenceAppService.UpdateAsync(input);
        await TriggerSettingChangedAsync();
    }

    public Task TriggerSettingChangedAsync()
    {
        return SettingChanged?.Invoke() ?? Task.CompletedTask;
    }

    protected virtual AntDesignThemePreferenceDto BuildDefaultPreference()
    {
        return new AntDesignThemePreferenceDto
        {
            ThemeSettingsEnabled = AntDesignThemeSettingDefaults.EnableThemeSettings,
            PageStyleSettingEnabled = AntDesignThemeSettingDefaults.EnablePageStyleSetting,
            NavigationModeSettingEnabled = AntDesignThemeSettingDefaults.EnableNavigationModeSetting,
            RegionalSettingsEnabled = AntDesignThemeSettingDefaults.EnableRegionalSettings,
            OtherSettingsEnabled = AntDesignThemeSettingDefaults.EnableOtherSettings,
            ThemeStyle = AntDesignThemeSettingDefaults.ThemeStyle,
            NavigationMode = AntDesignThemeSettingDefaults.NavigationMode,
            ContentWidth = AntDesignThemeSettingDefaults.ContentWidth,
            FixedHeader = AntDesignThemeSettingDefaults.FixedHeader,
            FixSiderbar = AntDesignThemeSettingDefaults.FixSiderbar,
            SplitMenus = AntDesignThemeSettingDefaults.SplitMenus,
            ShowHeader = AntDesignThemeSettingDefaults.ShowHeader,
            ShowFooter = AntDesignThemeSettingDefaults.ShowFooter,
            ShowMenu = AntDesignThemeSettingDefaults.ShowMenu,
            ShowMenuHeader = AntDesignThemeSettingDefaults.ShowMenuHeader,
            ColorWeak = AntDesignThemeSettingDefaults.ColorWeak
        };
    }
}

using System;
using System.Threading.Tasks;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Lsw.Abp.AntDesignThemeManagement.Localization;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;

namespace Lsw.Abp.AntDesignThemeManagement.Blazor.Pages.SettingManagement.ThemeSettingsManagementGroup;

public partial class ThemeSettingsManagementGroupViewComponent
{
    [Inject]
    protected IAntDesignThemePreferenceAppService AntDesignThemePreferenceAppService { get; set; } = default!;

    [Inject]
    protected IAntDesignSettingsProvider AntDesignSettingsProvider { get; set; } = default!;

    protected bool ThemeSettingsEnabled { get; set; }
    protected bool PageStyleSettingEnabled { get; set; }
    protected bool NavigationModeSettingEnabled { get; set; }
    protected bool RegionalSettingsEnabled { get; set; }
    protected bool OtherSettingsEnabled { get; set; }

    public ThemeSettingsManagementGroupViewComponent()
    {
        LocalizationResource = typeof(AntDesignThemeManagementResource);
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var preference = await AntDesignThemePreferenceAppService.GetAsync();
            ThemeSettingsEnabled = preference.ThemeSettingsEnabled;
            PageStyleSettingEnabled = preference.PageStyleSettingEnabled;
            NavigationModeSettingEnabled = preference.NavigationModeSettingEnabled;
            RegionalSettingsEnabled = preference.RegionalSettingsEnabled;
            OtherSettingsEnabled = preference.OtherSettingsEnabled;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual void OnThemeSettingsEnabledChanged(bool value)
    {
        ThemeSettingsEnabled = value;
        if (!value)
        {
            SetAllSubItems(false);
            return;
        }

        SetAllSubItems(true);
    }

    protected virtual void OnPageStyleSettingEnabledChanged(bool value)
    {
        PageStyleSettingEnabled = value;
        SyncRootSwitchByChildren();
    }

    protected virtual void OnNavigationModeSettingEnabledChanged(bool value)
    {
        NavigationModeSettingEnabled = value;
        SyncRootSwitchByChildren();
    }

    protected virtual void OnRegionalSettingsEnabledChanged(bool value)
    {
        RegionalSettingsEnabled = value;
        SyncRootSwitchByChildren();
    }

    protected virtual void OnOtherSettingsEnabledChanged(bool value)
    {
        OtherSettingsEnabled = value;
        SyncRootSwitchByChildren();
    }

    protected virtual async Task SaveAsync()
    {
        try
        {
            await AntDesignThemePreferenceAppService.UpdateThemeSettingsAvailabilityAsync(
                new UpdateAntDesignThemeSettingsAvailabilityDto
                {
                    ThemeSettingsEnabled = ThemeSettingsEnabled,
                    PageStyleSettingEnabled = PageStyleSettingEnabled,
                    NavigationModeSettingEnabled = NavigationModeSettingEnabled,
                    RegionalSettingsEnabled = RegionalSettingsEnabled,
                    OtherSettingsEnabled = OtherSettingsEnabled
                }
            );
            await AntDesignSettingsProvider.TriggerSettingChangedAsync();
            await Message.Success(L["SavedSuccessfully"]);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual void SetAllSubItems(bool enabled)
    {
        PageStyleSettingEnabled = enabled;
        NavigationModeSettingEnabled = enabled;
        RegionalSettingsEnabled = enabled;
        OtherSettingsEnabled = enabled;
    }

    protected virtual void SyncRootSwitchByChildren()
    {
        ThemeSettingsEnabled = PageStyleSettingEnabled
            || NavigationModeSettingEnabled
            || RegionalSettingsEnabled
            || OtherSettingsEnabled;
    }
}

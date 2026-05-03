using Lsw.Abp.AntDesignThemeManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Lsw.Abp.AntDesignThemeManagement.Settings;

public class AntDesignThemeManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.EnableThemeSettings,
                AntDesignThemeSettingDefaults.EnableThemeSettings.ToString().ToLowerInvariant(),
                L("Settings:EnableThemeSettings"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.EnablePageStyleSetting,
                AntDesignThemeSettingDefaults.EnablePageStyleSetting.ToString().ToLowerInvariant(),
                L("Settings:PageStyleSetting"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.EnableNavigationModeSetting,
                AntDesignThemeSettingDefaults.EnableNavigationModeSetting.ToString().ToLowerInvariant(),
                L("Settings:NavigationModeSetting"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.EnableRegionalSettings,
                AntDesignThemeSettingDefaults.EnableRegionalSettings.ToString().ToLowerInvariant(),
                L("Settings:RegionalSettings"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.EnableOtherSettings,
                AntDesignThemeSettingDefaults.EnableOtherSettings.ToString().ToLowerInvariant(),
                L("Settings:OtherSettings"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ThemeStyle,
                AntDesignThemeSettingDefaults.ThemeStyle,
                L("Settings:ThemeStyle"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.NavigationMode,
                AntDesignThemeSettingDefaults.NavigationMode,
                L("Settings:NavigationMode"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ContentWidth,
                AntDesignThemeSettingDefaults.ContentWidth,
                L("Settings:ContentWidth"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.FixedHeader,
                AntDesignThemeSettingDefaults.FixedHeader.ToString().ToLowerInvariant(),
                L("Settings:FixedHeader"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.FixSiderbar,
                AntDesignThemeSettingDefaults.FixSiderbar.ToString().ToLowerInvariant(),
                L("Settings:FixSiderbar"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.SplitMenus,
                AntDesignThemeSettingDefaults.SplitMenus.ToString().ToLowerInvariant(),
                L("Settings:SplitMenus"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ShowHeader,
                AntDesignThemeSettingDefaults.ShowHeader.ToString().ToLowerInvariant(),
                L("Settings:ShowHeader"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ShowFooter,
                AntDesignThemeSettingDefaults.ShowFooter.ToString().ToLowerInvariant(),
                L("Settings:ShowFooter"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ShowMenu,
                AntDesignThemeSettingDefaults.ShowMenu.ToString().ToLowerInvariant(),
                L("Settings:ShowMenu"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ShowMenuHeader,
                AntDesignThemeSettingDefaults.ShowMenuHeader.ToString().ToLowerInvariant(),
                L("Settings:ShowMenuHeader"),
                isVisibleToClients: true
            ),
            new SettingDefinition(
                AntDesignThemeManagementSettingNames.ColorWeak,
                AntDesignThemeSettingDefaults.ColorWeak.ToString().ToLowerInvariant(),
                L("Settings:ColorWeak"),
                isVisibleToClients: true
            )
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AntDesignThemeManagementResource>(name);
    }
}

namespace Lsw.Abp.AntDesignThemeManagement.Dtos;

public class AntDesignThemePreferenceDto
{
    public bool ThemeSettingsEnabled { get; set; }
    public bool PageStyleSettingEnabled { get; set; }
    public bool NavigationModeSettingEnabled { get; set; }
    public bool RegionalSettingsEnabled { get; set; }
    public bool OtherSettingsEnabled { get; set; }

    public string ThemeStyle { get; set; } = string.Empty;
    public string NavigationMode { get; set; } = string.Empty;
    public string ContentWidth { get; set; } = string.Empty;
    public bool FixedHeader { get; set; }
    public bool FixSiderbar { get; set; }
    public bool SplitMenus { get; set; }
    public bool ShowHeader { get; set; }
    public bool ShowFooter { get; set; }
    public bool ShowMenu { get; set; }
    public bool ShowMenuHeader { get; set; }
    public bool ColorWeak { get; set; }
}

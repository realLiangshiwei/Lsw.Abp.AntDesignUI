namespace Lsw.Abp.AntDesignThemeManagement.Dtos;

public class UpdateAntDesignThemeSettingsAvailabilityDto
{
    public bool ThemeSettingsEnabled { get; set; }
    public bool PageStyleSettingEnabled { get; set; }
    public bool NavigationModeSettingEnabled { get; set; }
    public bool RegionalSettingsEnabled { get; set; }
    public bool OtherSettingsEnabled { get; set; }
}

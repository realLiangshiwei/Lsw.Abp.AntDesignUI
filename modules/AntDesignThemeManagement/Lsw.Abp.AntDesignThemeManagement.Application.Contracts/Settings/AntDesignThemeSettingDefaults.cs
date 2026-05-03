namespace Lsw.Abp.AntDesignThemeManagement.Settings;

public static class AntDesignThemeSettingDefaults
{
    public const bool EnableThemeSettings = true;
    public const bool EnablePageStyleSetting = true;
    public const bool EnableNavigationModeSetting = true;
    public const bool EnableRegionalSettings = true;
    public const bool EnableOtherSettings = true;

    public const string ThemeStyle = ThemeStyles.Light;
    public const string NavigationMode = NavigationModes.Side;
    public const string ContentWidth = ContentWidths.Fluid;
    public const bool FixedHeader = true;
    public const bool FixSiderbar = true;
    public const bool SplitMenus = false;
    public const bool ShowHeader = true;
    public const bool ShowFooter = true;
    public const bool ShowMenu = true;
    public const bool ShowMenuHeader = true;
    public const bool ColorWeak = false;
}

public static class ThemeStyles
{
    public const string Light = "Light";
    public const string Dark = "Dark";
    public const string RealDark = "RealDark";
}

public static class NavigationModes
{
    public const string Side = "Side";
    public const string Top = "Top";
    public const string Mix = "Mix";
}

public static class ContentWidths
{
    public const string Fluid = "Fluid";
    public const string Fixed = "Fixed";
}

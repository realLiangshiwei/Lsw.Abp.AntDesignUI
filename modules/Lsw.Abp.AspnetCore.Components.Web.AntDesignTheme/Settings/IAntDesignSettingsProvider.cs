using System;
using System.Threading.Tasks;
using AntDesign;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;

public interface IAntDesignSettingsProvider
{
    Task<MenuPlacement> GetMenuPlacementAsync();

    Task<MenuTheme> GetMenuThemeAsync();

    Task TriggerSettingChanged();
    
    public event AntDesignSettingsProvider.AntDesignSettingChangedHandler SettingChanged;
}

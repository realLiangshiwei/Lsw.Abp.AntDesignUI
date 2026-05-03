using System;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignThemeManagement.Dtos;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;

public interface IAntDesignSettingsProvider
{
    Task<AntDesignThemePreferenceDto> GetPreferenceAsync();

    Task<MenuPlacement> GetMenuPlacementAsync();

    Task<MenuTheme> GetMenuThemeAsync();

    Task ApplyPreferenceAsync(UpdateAntDesignThemePreferenceDto input);

    Task TriggerSettingChangedAsync();

    event Func<Task>? SettingChanged;
}

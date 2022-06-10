using System;
using System.Security.AccessControl;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.Core.Helpers.MemberPath;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;

public class AntDesignSettingsProvider : IAntDesignSettingsProvider, IScopedDependency
{
    //TODO use SettingProvider instead of AbpAntDesignThemeOptions
    // [Inject]
    // protected ISettingProvider SettingProvider { get; set; }
    
    [Inject]
    public IOptions<AbpAntDesignThemeOptions> Options { get; set; }
    
    public delegate Task AntDesignSettingChangedHandler();
    
    public event AntDesignSettingChangedHandler SettingChanged;

    public Task<MenuPlacement> GetMenuPlacementAsync()
    {
        return Task.FromResult(Options.Value.Menu.Placement);
    }

    public Task<MenuTheme> GetMenuThemeAsync()
    {
        return Task.FromResult(Options.Value.Menu.Theme);
    }

    public Task TriggerSettingChanged()
    {
        return SettingChanged?.Invoke();
    }
}

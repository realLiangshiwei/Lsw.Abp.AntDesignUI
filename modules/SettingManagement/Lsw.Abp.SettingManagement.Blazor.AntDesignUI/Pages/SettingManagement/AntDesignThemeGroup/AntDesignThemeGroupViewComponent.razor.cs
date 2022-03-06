using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.SettingManagement.Localization;

namespace Lsw.Abp.SettingManagement.Blazor.AntDesignUI.Pages.SettingManagement.AntDesignThemeGroup;


//TODO  localization
public partial class AntDesignThemeGroupViewComponent
{
    public const string Name = "Volo.Abp.AntDesignThemeGroupViewComponent";
    
    [Inject]
    protected IOptions<AbpAntDesignThemeOptions> Options { get; set; }

    [Inject]
    protected IAntDesignSettingsProvider AntDesignSettingsProvider { get; set; }
    
    [CascadingParameter]
    protected SettingManagement SettingManagement { get; set; }

    protected AbpAntDesignThemeOptions AbpAntDesignThemeOptions { get; set; } = new();

    protected readonly List<SelectOptionItem> MenuPlacements = new()
    {
        new SelectOptionItem("Left",MenuPlacement.Left),
        new SelectOptionItem("Top",MenuPlacement.Top)
    };
    
    protected readonly List<SelectOptionItem> MenuThemes = new()
    {
        new SelectOptionItem("Dark",MenuTheme.Dark),
        new SelectOptionItem("Light",MenuTheme.Light)
    };
    
    public AntDesignThemeGroupViewComponent()
    {
        LocalizationResource = typeof(AbpSettingManagementResource);
    }

    protected override void OnInitialized()
    {
        AbpAntDesignThemeOptions = Options.Value;
    }

    private async Task UpdateSettingAsync()
    {
        await AntDesignSettingsProvider.TriggerSettingChanged();

        await Notify.Success(L["SuccessfullySaved"]);
    }
}

public class SelectOptionItem
{
    public SelectOptionItem(string text, object value)
    {
        Text = text;
        Value = value;
    }
    
    public string Text { get; set; }

    public object Value { get; set; }
}

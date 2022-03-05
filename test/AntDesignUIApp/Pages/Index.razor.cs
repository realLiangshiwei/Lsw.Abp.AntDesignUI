using AntDesign;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.AspNetCore.Components.Progression;
using BreadcrumbItem = Lsw.Abp.AntDesignUI.BreadcrumbItem;

namespace AntDesignUIApp.Pages;

public partial class Index : ComponentBase
{
    public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new();

    public PageToolbar Toolbar { get; set; } = new(); 
    
    [Inject]
    public IUiNotificationService NotificationService { get; set; }
    
    [Inject]
    public IUiPageProgressService UiPageProgressService { get; set; }
    
    [Inject]
    protected IOptions<AbpAntDesignThemeOptions> Options { get; set; }
    
    [Inject]
    protected IAntDesignSettingsProvider AntDesignSettingsProvider { get; set; }

    protected override void OnInitialized()
    {
        BreadcrumbItems = new List<BreadcrumbItem>()
        {
            new("Index")
        };
        
        Toolbar.AddButton("Notification", () => NotificationService.Info("new item!"), "plus");
        Toolbar.AddButton("Page progress", async () =>
        {
            var i = 0;

            while (i < 100)
            {
                i++;
                await UiPageProgressService.Go(i);
                await Task.Delay(50);
            }
            
            await UiPageProgressService.Go(null);
        }, "double-right");
    }

    private async Task ChangeMenuPlacement(MenuPlacement menuPlacement)
    {
        Options.Value.Menu.Placement = menuPlacement;
        await AntDesignSettingsProvider.TriggerSettingChanged();
    }
    
    private async Task ChangeMenuTheme(MenuTheme menuTheme)
    {
        Options.Value.Menu.Theme = menuTheme;
        await AntDesignSettingsProvider.TriggerSettingChanged();
    }
}

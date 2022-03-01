using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.AspNetCore.Components.Progression;

namespace AntDesignUIApp.Pages;

public partial class Index : ComponentBase
{
    public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new();

    public PageToolbar Toolbar { get; set; } = new(); 
    
    [Inject]
    public IUiNotificationService NotificationService { get; set; }
    
    [Inject]
    public IUiPageProgressService UiPageProgressService { get; set; }

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
}

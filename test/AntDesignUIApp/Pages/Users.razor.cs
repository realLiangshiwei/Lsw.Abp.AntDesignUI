using Lsw.Abp.AntDesignUI;
using Microsoft.AspNetCore.Components;

namespace AntDesignUIApp.Pages;

public partial class Users : ComponentBase
{
    public List<AbpBreadcrumbItem> BreadcrumbItems { get; set; } = new();
    
    protected override void OnInitialized()
    {
        BreadcrumbItems = new List<AbpBreadcrumbItem>()
        {
            new("Admin"),
            new("Users")
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignUI;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.SettingManagement.Blazor.AntDesignUI.Pages.SettingManagement.AntDesignThemeGroup;
using Volo.Abp.SettingManagement.Localization;

namespace Volo.Abp.SettingManagement.Blazor.AntDesignUI.Pages.SettingManagement;

public partial class SettingManagement
{
    [Inject]
    protected IServiceProvider ServiceProvider { get; set; }

    protected SettingComponentCreationContext SettingComponentCreationContext { get; set; }

    [Inject]
    protected IOptions<SettingManagementComponentOptions> _options { get; set; }
    
    [Inject]
    protected IStringLocalizer<AbpSettingManagementResource> L { get; set; }

    protected SettingManagementComponentOptions Options => _options.Value;

    protected List<RenderFragment> SettingItemRenders { get; set; } = new();

    protected string SelectedGroup { get; set; }
    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();


    protected override async Task OnInitializedAsync()
    {
        BreadcrumbItems = new List<AbpBreadcrumbItem>
        {
            new(L["Settings"])
        };
        
        SettingComponentCreationContext = new SettingComponentCreationContext(ServiceProvider);

        foreach (var contributor in Options.Contributors)
        {
            await contributor.ConfigureAsync(SettingComponentCreationContext);
        }

        SettingItemRenders.Clear();
    }

    protected virtual string GetNormalizedString(string value)
    {
        return value.Replace('.', '_');
    }
}

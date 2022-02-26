using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Microsoft.AspNetCore.Components;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Layout;

public partial class AbpPageHeader : ComponentBase
{
    protected List<RenderFragment> ToolbarItemRenders { get; set; }

    public IPageToolbarManager PageToolbarManager { get; set; }

    [Inject]
    public PageLayout PageLayout { get; private set; }

    [Parameter]
    public string Title { get => PageLayout.Title; set => PageLayout.Title = value; }

    [Parameter]
    public bool BreadcrumbShowHome { get; set; } = true;

    [Parameter]
    public bool BreadcrumbShowCurrent { get; set; } = true;

    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    [Parameter] 
    public List<BreadcrumbItem> BreadcrumbItems {
        get => PageLayout.BreadcrumbItems.ToList();
        set => PageLayout.BreadcrumbItems = new ObservableCollection<BreadcrumbItem>(value);
    }
    
    [Parameter]
    public PageToolbar Toolbar { get; set; }

    public AbpPageHeader()
    {
        ToolbarItemRenders = new List<RenderFragment>();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (Toolbar != null)
        {
            var toolbarItems = await PageToolbarManager.GetItemsAsync(Toolbar);
            ToolbarItemRenders.Clear();

            if (!Options.Value.RenderToolbar)
            {
                PageLayout.ToolbarItems.Clear();
                foreach (var item in toolbarItems)
                {
                    PageLayout.ToolbarItems.Add(item);
                }
                return;
            }

            foreach (var item in toolbarItems)
            {
                var sequence = 0;
                ToolbarItemRenders.Add(builder =>
                {
                    builder.OpenComponent(sequence, item.ComponentType);
                    if (item.Arguments != null)
                    {
                        foreach (var argument in item.Arguments)
                        {
                            sequence++;
                            builder.AddAttribute(sequence, argument.Key, argument.Value);
                        }
                    }
                    builder.CloseComponent();
                });
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}

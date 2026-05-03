using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class MainMenuItem : ComponentBase
{
    [Parameter]
    public ApplicationMenuItem Menu { get; set; } = default!;

    [Parameter]
    public EventCallback OnItemClick { get; set; }

    protected virtual Task OnClickAsync()
    {
        return OnItemClick.HasDelegate
            ? OnItemClick.InvokeAsync()
            : Task.CompletedTask;
    }
}

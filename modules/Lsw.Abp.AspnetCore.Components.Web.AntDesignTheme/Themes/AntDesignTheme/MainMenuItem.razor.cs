using AntDesign;
using Microsoft.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class MainMenuItem : ComponentBase
{
    [Parameter]
    public ApplicationMenuItem Menu { get; set; }

    private RenderFragment GetSubMenuTemplate()
    {
        return null;
        // return @<span>
        //     @if (Menu.Icon != null)
        // {
        //     <Icon Type="@Menu.Icon" Theme="outline"></Icon>
        // }
        // <span>@Menu.Name</span>
        //     </span>;
    }
}

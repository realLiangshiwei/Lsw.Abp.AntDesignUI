namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;

public class PageToolbar
{
    public PageToolbarContributorList Contributors { get; set; }

    public PageToolbar()
    {
        Contributors = new PageToolbarContributorList();
    }
}

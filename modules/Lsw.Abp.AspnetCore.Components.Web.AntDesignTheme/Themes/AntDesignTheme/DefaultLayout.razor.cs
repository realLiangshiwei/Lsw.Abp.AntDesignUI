namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme;

public partial class DefaultLayout
{
    protected bool Collapsed { get; set; }

    protected void OnCollapse()
    {
        Collapsed = !Collapsed;
    }
}

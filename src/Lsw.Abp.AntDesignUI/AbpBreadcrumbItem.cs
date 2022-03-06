namespace Lsw.Abp.AntDesignUI;

public class AbpBreadcrumbItem
{
    public string Text { get; set; }

    public string Icon { get; set; }

    public string Url { get; set; }

    public AbpBreadcrumbItem(string text, string url = null, string icon = null)
    {
        Text = text;
        Url = url;
        Icon = icon;
    }
}

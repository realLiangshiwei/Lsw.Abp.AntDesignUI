using AntDesign;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;

public class AbpAntDesignThemeOptions
{
    public MenuOptions Menu { get; set; }
}

public class MenuOptions
{
    public MenuTheme Theme { get; set; }

    public MenuPlace Place { get; set; }

    public MenuMode GetMode()
    {
        return Place == MenuPlace.Left ? MenuMode.Inline : MenuMode.Horizontal;
    }
    
    public enum MenuPlace
    {
        Top,
        Left
    }
}

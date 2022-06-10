using System.Threading.Tasks;
using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Themes.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

namespace Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;


public class AntDesignThemeToolbarContributor: IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
        }

        return Task.CompletedTask;
    }
}

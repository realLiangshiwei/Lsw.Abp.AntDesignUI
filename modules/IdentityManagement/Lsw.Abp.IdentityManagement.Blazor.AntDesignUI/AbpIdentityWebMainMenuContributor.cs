using System.Threading.Tasks;
using AntDesign;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;

public class AbpIdentityWebMainMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administrationMenu = context.Menu.GetAdministration();
        administrationMenu.Icon = IconType.Outline.PicLeft;

        var l = context.GetLocalizer<IdentityResource>();

        var identityMenuItem = new ApplicationMenuItem(IdentityMenuNames.GroupName, l["Menu:IdentityManagement"],
            icon: IconType.Outline.User);
        administrationMenu.AddItem(identityMenuItem);

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.Roles,
            l["Roles"],
            url: "~/identity/roles").RequirePermissions(IdentityPermissions.Roles.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.Users,
            l["Users"],
            url: "~/identity/users").RequirePermissions(IdentityPermissions.Users.Default));

        return Task.CompletedTask;
    }
}

using BookStoreWebApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BookStoreWebApp.Permissions;

public class BookStoreWebAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookStoreWebAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStoreWebAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreWebAppResource>(name);
    }
}

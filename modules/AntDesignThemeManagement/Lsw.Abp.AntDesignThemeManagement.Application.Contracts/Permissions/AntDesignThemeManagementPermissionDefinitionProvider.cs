using Lsw.Abp.AntDesignThemeManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lsw.Abp.AntDesignThemeManagement.Permissions;

public class AntDesignThemeManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(
            AntDesignThemeManagementPermissions.GroupName,
            L("Permission:AntDesignThemeManagement")
        );

        group.AddPermission(
            AntDesignThemeManagementPermissions.Settings,
            L("Permission:AntDesignThemeManagement.Settings")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AntDesignThemeManagementResource>(name);
    }
}

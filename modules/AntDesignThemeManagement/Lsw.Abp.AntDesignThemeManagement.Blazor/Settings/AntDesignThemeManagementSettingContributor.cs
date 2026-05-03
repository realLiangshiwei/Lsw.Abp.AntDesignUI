using System.Threading.Tasks;
using Lsw.Abp.AntDesignThemeManagement.Blazor.Pages.SettingManagement.ThemeSettingsManagementGroup;
using Lsw.Abp.AntDesignThemeManagement.Localization;
using Lsw.Abp.AntDesignThemeManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement.Blazor;

namespace Lsw.Abp.AntDesignThemeManagement.Blazor.Settings;

public class AntDesignThemeManagementSettingContributor : ISettingComponentContributor
{
    public virtual async Task ConfigureAsync(SettingComponentCreationContext context)
    {
        if (!await CheckPermissionsAsync(context))
        {
            return;
        }

        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AntDesignThemeManagementResource>>();
        context.Groups.Add(
            new SettingComponentGroup(
                "Lsw.AntDesignThemeManagement",
                l["Menu:ThemeSettingsManagement"],
                typeof(ThemeSettingsManagementGroupViewComponent)
            )
        );
    }

    public virtual async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
        return await authorizationService.IsGrantedAsync(AntDesignThemeManagementPermissions.Settings);
    }
}

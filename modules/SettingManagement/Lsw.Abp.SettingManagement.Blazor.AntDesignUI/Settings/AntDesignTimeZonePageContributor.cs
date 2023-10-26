using System.Threading.Tasks;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI.Pages.SettingManagement.TimeZoneSettingGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.Timing;

namespace Lsw.Abp.SettingManagement.Blazor.AntDesignUI.Settings;

public class AntDesignTimeZonePageContributor : ISettingComponentContributor
{
    public virtual async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(SettingManagementPermissions.TimeZone);
    }

    public virtual Task ConfigureAsync(SettingComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AbpSettingManagementResource>>();
        if (context.ServiceProvider.GetRequiredService<IClock>().SupportsMultipleTimezone)
        {
            context.Groups.Add(
                new SettingComponentGroup(
                    "Volo.Abp.TimeZone",
                    l["Menu:TimeZone"],
                    typeof(TimeZoneSettingGroupViewComponent)
                )
            );
        }

        return Task.CompletedTask;
    }
}

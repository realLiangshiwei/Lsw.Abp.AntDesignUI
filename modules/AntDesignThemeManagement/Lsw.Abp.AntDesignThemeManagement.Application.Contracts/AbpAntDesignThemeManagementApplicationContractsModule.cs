using Lsw.Abp.AntDesignThemeManagement.Localization;
using Lsw.Abp.AntDesignThemeManagement.Settings;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;

namespace Lsw.Abp.AntDesignThemeManagement;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpSettingManagementApplicationContractsModule)
)]
public class AbpAntDesignThemeManagementApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAntDesignThemeManagementApplicationContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AntDesignThemeManagementResource>("en")
                .AddVirtualJson("/Localization/Resources/AntDesignThemeManagement");
        });
    }
}

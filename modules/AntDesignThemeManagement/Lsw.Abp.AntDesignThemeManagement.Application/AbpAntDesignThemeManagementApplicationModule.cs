using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Lsw.Abp.AntDesignThemeManagement;

[DependsOn(
    typeof(AbpAntDesignThemeManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationModule)
)]
public class AbpAntDesignThemeManagementApplicationModule : AbpModule
{
}

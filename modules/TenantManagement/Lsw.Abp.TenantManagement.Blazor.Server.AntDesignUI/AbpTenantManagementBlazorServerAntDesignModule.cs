using Lsw.Abp.FeatureManagement.Blazor.Server.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;

namespace Lsw.Abp.TenantManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpTenantManagementBlazorAntDesignModule),
    typeof(AbpFeatureManagementBlazorServerAntDesignModule)
)]
public class AbpTenantManagementBlazorServerAntDesignModule : AbpModule
{
    
}

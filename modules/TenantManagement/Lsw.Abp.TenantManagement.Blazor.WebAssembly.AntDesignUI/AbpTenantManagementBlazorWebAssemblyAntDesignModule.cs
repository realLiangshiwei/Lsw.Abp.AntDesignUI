using Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI;
using Lsw.Abp.TenantManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI;


[DependsOn(
    typeof(AbpTenantManagementBlazorAntDesignModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpTenantManagementHttpApiClientModule)
)]
public class AbpTenantManagementBlazorWebAssemblyAntDesignModule : AbpModule
{
    
}

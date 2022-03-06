using Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;
using Lsw.Abp.PermissionManagement.Blazor.WebAssembly.AntDesignUI;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI;

[DependsOn(
    typeof(AbpIdentityBlazorAntDesignModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyAntDesignModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AbpIdentityBlazorWebAssemblyAntDesignModule: AbpModule
{
}

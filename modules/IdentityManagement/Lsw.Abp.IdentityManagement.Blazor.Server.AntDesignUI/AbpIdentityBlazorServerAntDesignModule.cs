using Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;
using Lsw.Abp.PermissionManagement.Blazor.AntDesignUI;
using Volo.Abp.Modularity;

namespace Lsw.Abp.IdentityManagement.Blazor.Server.AntDesignUI;

[DependsOn(
    typeof(AbpIdentityBlazorAntDesignModule),
    typeof(AbpPermissionManagementBlazorAntDesignModule)
)]
public class AbpIdentityBlazorServerAntDesignModule : AbpModule
{
    
}

using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.FeatureManagement.Blazor.AntDesignUI;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.TenantManagement.Blazor.AntDesignUI;


[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpFeatureManagementBlazorAntDesignModule)
)]
public class AbpTenantManagementBlazorAntDesignModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpTenantManagementBlazorAntDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpTenantManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpTenantManagementBlazorAntDesignModule).Assembly);
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    TenantManagementModuleExtensionConsts.ModuleName,
                    TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                    createFormTypes: new[] { typeof(TenantCreateDto) },
                    editFormTypes: new[] { typeof(TenantUpdateDto) }
                );
        });
    }
}

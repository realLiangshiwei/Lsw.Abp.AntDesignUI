using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Lsw.Abp.PermissionManagement.Blazor.AntDesignUI;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.IdentityManagement.Blazor.AntDesignUI;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementBlazorAntDesignModule),
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule),
    typeof(AbpAntDesignUIModule)
)]
public class AbpIdentityBlazorAntDesignModule: AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpIdentityBlazorAntDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpIdentityBlazorAntDesignAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new AbpIdentityWebMainMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpIdentityBlazorAntDesignModule).Assembly);
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.Role,
                    createFormTypes: new[] { typeof(IdentityRoleCreateDto) },
                    editFormTypes: new[] { typeof(IdentityRoleUpdateDto) }
                );

            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    createFormTypes: new[] { typeof(IdentityUserCreateDto) },
                    editFormTypes: new[] { typeof(IdentityUserUpdateDto) }
                );
        });
    }
}

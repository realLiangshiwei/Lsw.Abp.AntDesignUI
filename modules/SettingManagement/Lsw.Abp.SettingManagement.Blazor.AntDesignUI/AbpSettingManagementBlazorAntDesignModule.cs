using AutoMapper;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Lsw.Abp.SettingManagement.Blazor.AntDesignUI.Settings;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.UI.Navigation;

namespace Lsw.Abp.SettingManagement.Blazor.AntDesignUI;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule)
)]
public class AbpSettingManagementBlazorAntDesignModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpSettingManagementBlazorAntDesignModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<SettingManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SettingManagementMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpSettingManagementBlazorAntDesignModule).Assembly);
        });

        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new AntDesignSettingDefultPageContributor());
            options.Contributors.Add(new AntDesignTimeZonePageContributor());
        });
    }
}

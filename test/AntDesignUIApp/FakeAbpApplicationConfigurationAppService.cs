using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Volo.Abp.AspNetCore.Mvc.MultiTenancy;
using Volo.Abp.DependencyInjection;

namespace AntDesignUIApp;

public class FakeAbpApplicationConfigurationAppService : AbpApplicationConfigurationClientProxy
{
    public override Task<ApplicationConfigurationDto> GetAsync()
    {
        var result = new ApplicationConfigurationDto()
        {
            Setting =new ApplicationSettingConfigurationDto(),
            Auth = new ApplicationAuthConfigurationDto(),
            Clock = new ClockDto(),
            CurrentTenant = new CurrentTenantDto(),
            CurrentUser = new CurrentUserDto()
            {
                Name = "admin",
                UserName = "admin"
            },
            Features = new ApplicationFeatureConfigurationDto(),
            Localization = new ApplicationLocalizationConfigurationDto(),
            MultiTenancy = new MultiTenancyInfoDto(),
            ObjectExtensions = new ObjectExtensionsDto(),
            Timing = new TimingDto()
        };
        return Task.FromResult(result);
    }
}

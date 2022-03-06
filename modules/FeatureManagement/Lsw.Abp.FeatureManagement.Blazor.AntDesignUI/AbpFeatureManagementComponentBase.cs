using Volo.Abp.AspNetCore.Components;
using Volo.Abp.FeatureManagement.Localization;

namespace Lsw.Abp.FeatureManagement.Blazor.AntDesignUI;

public abstract class AbpFeatureManagementComponentBase : AbpComponentBase
{
    protected AbpFeatureManagementComponentBase()
    {
        LocalizationResource = typeof(AbpFeatureManagementResource);
    }
}

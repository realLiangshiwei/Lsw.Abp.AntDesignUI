using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Volo.Abp.SettingManagement.Blazor;

public class SettingManagementComponentOptions
{
    public List<ISettingComponentContributor> Contributors { get; }

    public SettingManagementComponentOptions()
    {
        Contributors = new List<ISettingComponentContributor>();
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Bundling;
using Volo.Abp.AspNetCore.Bundling;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Bundling;

public class BlazorServerComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    protected IBundleManager BundleManager { get; }

    public BlazorServerComponentBundleManager(IBundleManager bundleManager)
    {
        BundleManager = bundleManager;
    }

    public virtual async Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetStyleBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }

    public virtual async Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetScriptBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Bundling;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme.Bundling;

public class WebAssemblyComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    public virtual Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }

    public virtual Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }
}

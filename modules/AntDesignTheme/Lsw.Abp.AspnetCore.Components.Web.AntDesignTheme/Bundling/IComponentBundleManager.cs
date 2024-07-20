using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Bundling;

public interface IComponentBundleManager
{
    Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName);

    Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName);
}

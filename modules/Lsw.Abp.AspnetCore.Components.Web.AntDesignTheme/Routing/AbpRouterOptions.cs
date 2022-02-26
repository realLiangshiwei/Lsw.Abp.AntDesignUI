using System.Reflection;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing;

public class AbpRouterOptions
{
    public Assembly AppAssembly { get; set; }

    public RouterAssemblyList AdditionalAssemblies { get; }

    public AbpRouterOptions()
    {
        AdditionalAssemblies = new RouterAssemblyList();
    }
}

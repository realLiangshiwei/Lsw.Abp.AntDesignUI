using System;
using System.Threading.Tasks;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

public class ToolbarManager : IToolbarManager, ITransientDependency
{
    protected AbpToolbarOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }

    public ToolbarManager(
        IOptions<AbpToolbarOptions> options,
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
    }

    public async Task<Toolbar> GetAsync(string name)
    {
        var toolbar = new Toolbar(name);

        using (var scope = ServiceProvider.CreateScope())
        {
            var context = new ToolbarConfigurationContext(toolbar, scope.ServiceProvider);

            foreach (var contributor in Options.Contributors)
            {
                await contributor.ConfigureToolbarAsync(context);
            }
        }

        return toolbar;
    }
}

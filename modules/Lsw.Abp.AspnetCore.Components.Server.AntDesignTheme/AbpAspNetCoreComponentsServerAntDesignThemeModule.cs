using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Bundling;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Server;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Volo.Abp.Modularity;

namespace Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerModule),
    typeof(AbpAspNetCoreMvcUiPackagesModule),
    typeof(AbpAspNetCoreMvcUiBundlingModule),
    typeof(AbpAspNetCoreComponentsWebAntDesignThemeModule)
)]
public class AbpAspNetCoreComponentsServerAntDesignThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorStandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                });
            
            options
                .StyleBundles
                .Add(BlazorAntDesignThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorAntDesignThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorAntDesignThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorAntDesignThemeScriptContributor));
                });
        });
        
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new AntDesignThemeToolbarContributor());
        });
    }
}

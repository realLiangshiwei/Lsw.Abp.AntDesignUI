using Volo.Abp.Bundling;

namespace Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme;

public class ComponentsComponentsBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/lang-utils.js");
        context.Add("_content/AntDesign/js/ant-design-blazor.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("_content/AntDesign/css/ant-design-blazor.css");
        context.Add("_content/Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme/libs/abp/css/theme.css");
    }
}

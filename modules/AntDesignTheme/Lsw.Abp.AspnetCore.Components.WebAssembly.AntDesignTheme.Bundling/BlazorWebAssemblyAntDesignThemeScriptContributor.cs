using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme.Bundling;

public class BlazorWebAssemblyAntDesignThemeScriptContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("_content/AntDesign/js/ant-design-blazor.js");
    }
}
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Bundling;

public class BlazorAntDesignThemeScriptContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/AntDesign/js/ant-design-blazor.js");
    }
}

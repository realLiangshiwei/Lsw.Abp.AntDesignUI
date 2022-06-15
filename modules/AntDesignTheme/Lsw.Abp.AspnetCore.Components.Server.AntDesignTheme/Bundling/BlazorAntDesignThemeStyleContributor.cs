using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Bundling;

public class BlazorAntDesignThemeStyleContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme/libs/abp/css/theme.css");
    }
}

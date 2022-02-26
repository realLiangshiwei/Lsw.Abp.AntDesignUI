using System.Collections.Generic;
using JetBrains.Annotations;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

public class AbpToolbarOptions
{
    [NotNull]
    public List<IToolbarContributor> Contributors { get; }

    public AbpToolbarOptions()
    {
        Contributors = new List<IToolbarContributor>();
    }
}

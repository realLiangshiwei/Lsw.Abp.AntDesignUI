using System;
using System.Collections.Generic;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme;

public class AbpDynamicLayoutComponentOptions
{
    /// <summary>
    /// Used to define components that renders in the layout
    /// </summary>
    public Dictionary<Type, IDictionary<string,object>?> Components { get; set; }

    public AbpDynamicLayoutComponentOptions()
    {
        Components = new Dictionary<Type, IDictionary<string, object>?>();
    }
}
﻿using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

public class ToolbarItem
{
    public Type ComponentType {
        get => _componentType;
        set => _componentType = Check.NotNull(value, nameof(value));
    }
    private Type _componentType;

    public int Order { get; set; }

    public ToolbarItem([NotNull] Type componentType, int order = 0)
    {
        Order = order;
        ComponentType = Check.NotNull(componentType, nameof(componentType));
    }
}

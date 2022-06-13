using System;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;

namespace Lsw.Abp.AntDesignUI.Components.ObjectExtending;

public static class EnumHelper
{
    public static string GetLocalizedMemberName(Type enumType, object value, IStringLocalizerFactory stringLocalizerFactory)
    {
        var memberName = enumType.GetEnumName(value);
        var localizedMemberName = AbpInternalLocalizationHelper.LocalizeWithFallback(
            new[]
            {
                        stringLocalizerFactory.CreateDefaultOrNull()
            },
            new[]
            {
                        $"Enum:{enumType.Name}.{memberName}",
                        $"{enumType.Name}.{memberName}",
                        memberName
            },
            memberName
        );

        return localizedMemberName;
    }
}

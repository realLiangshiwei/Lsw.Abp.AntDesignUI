using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.ObjectExtending;

namespace Lsw.Abp.AntDesignUI;

public static class ObjectExtensionPropertyInfoBlazorExtensions
{
    private static readonly Type[] DateTimeTypes =
    {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?)
        };

    public static bool IsDate(this IBasicObjectExtensionPropertyInfo property)
    {
        return DateTimeTypes.Contains(property.Type) &&
               property.GetDataTypeOrNull() == DataType.Date;
    }

    public static bool IsDateTime(this IBasicObjectExtensionPropertyInfo property)
    {
        return DateTimeTypes.Contains(property.Type) &&
               !property.IsDate();
    }

    public static DataType? GetDataTypeOrNull(this IBasicObjectExtensionPropertyInfo property)
    {
        return property
            .Attributes
            .OfType<DataTypeAttribute>()
            .FirstOrDefault()?.DataType;
    }
}

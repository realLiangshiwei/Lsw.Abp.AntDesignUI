using System.Collections.Generic;
using Volo.Abp.Data;

namespace Lsw.Abp.AntDesignUI.Components.ObjectExtending;

public partial class SelectExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected List<SelectItem<int>> SelectItems = new();

    public int SelectedValue {
        get { return Entity.GetProperty<int>(PropertyInfo.Name); }
        set { Entity.SetProperty(PropertyInfo.Name, value, false); }
    }

    protected virtual List<SelectItem<int>> GetSelectItemsFromEnum()
    {
        var selectItems = new List<SelectItem<int>>();

        foreach (var enumValue in PropertyInfo.Type.GetEnumValues())
        {
            selectItems.Add(new SelectItem<int>
            {
                Value = (int)enumValue,
                Text = EnumLocalizer.GetString(PropertyInfo.Type, enumValue)
            });
        }

        return selectItems;
    }

    protected override void OnParametersSet()
    {
        SelectItems = GetSelectItemsFromEnum();
        StateHasChanged();

        if (!Entity.HasProperty(PropertyInfo.Name))
        {
            SelectedValue = (int)PropertyInfo.Type.GetEnumValues().GetValue(0);
        }
    }
}

public class SelectItem<TValue>
{
    public string Text { get; set; }
    public TValue Value { get; set; }
    
    public override string ToString()
    {
        return Text;
    }
}

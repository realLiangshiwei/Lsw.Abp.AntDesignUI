using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility;
using Volo.Abp.Data;

namespace Lsw.Abp.AntDesignUI.Components.ObjectExtending;

public partial class LookupExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected List<SelectItem<object>> lookupItems;

    [Inject] public ILookupApiRequestService LookupApiService { get; set; }

    public string TextPropertyName => PropertyInfo.Name + "_Text";

    private string SelectedValue { get; set; }

    private Func<object, object, bool> _compareWith = (a, b) =>
    {
        if (a is SelectItem<object> o1 && b is SelectItem<object> o2)
        {
            return o1.Value == o2.Value;
        }

        return false;
    };

    public LookupExtensionProperty()
    {
        lookupItems = new List<SelectItem<object>>();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await SearchFilterChangedAsync(string.Empty);

        SelectedValue = Entity.GetProperty(TextPropertyName)?.ToString();
    }

    protected virtual void UpdateLookupTextProperty(object value)
    {
        var selectedItemText = lookupItems.SingleOrDefault(t => t.Value.Equals(value))?.Text;
        Entity.SetProperty(TextPropertyName, selectedItemText);
    }

    protected virtual async Task<List<SelectItem<object>>> GetLookupItemsAsync(string filter)
    {
        var selectItems = new List<SelectItem<object>>();

        var url = PropertyInfo.Lookup.Url;
        if (!filter.IsNullOrEmpty())
        {
            url += $"?{PropertyInfo.Lookup.FilterParamName}={filter.Trim()}";
        }

        var response = await LookupApiService.SendAsync(url);

        var document = JsonDocument.Parse(response);
        var itemsArrayProp = document.RootElement.GetProperty(PropertyInfo.Lookup.ResultListPropertyName);
        foreach (var item in itemsArrayProp.EnumerateArray())
        {
            selectItems.Add(new SelectItem<object>
            {
                Text = item.GetProperty(PropertyInfo.Lookup.DisplayPropertyName).GetString(),
                Value = JsonSerializer.Deserialize(item.GetProperty(PropertyInfo.Lookup.ValuePropertyName).GetRawText(),
                    PropertyInfo.Type)
            });
        }

        return selectItems;
    }

    private void SelectedValueChanged(AutoCompleteOption selectedItem)
    {
        var selectedObject = (selectedItem.Value as SelectItem<object>)?.Value;
        Entity.SetProperty(PropertyInfo.Name, selectedObject, false);
        UpdateLookupTextProperty(selectedObject);
    }

    protected virtual async Task SearchFilterChangedAsync(string filter)
    {
        lookupItems = await GetLookupItemsAsync(filter);
    }
}
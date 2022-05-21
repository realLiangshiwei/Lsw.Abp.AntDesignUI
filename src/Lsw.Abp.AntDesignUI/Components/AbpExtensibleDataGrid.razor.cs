using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Lsw.Abp.AntDesignUI.Components;

public partial class AbpExtensibleDataGrid<TItem> : ComponentBase
{
    protected const string DataFieldAttributeName = "Data";
    
    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

    protected Dictionary<string, TableEntityActionsColumn<TItem>> ActionColumns = new();
    
    [Parameter] 
    public IEnumerable<TItem> Data { get; set; }
    
    [Parameter]
    public int TotalItems { get; set; }

    [Parameter]
    public int PageSize { get; set; }

    [Parameter]
    public IEnumerable<TableColumn> Columns { get; set; }
    
    [Parameter]
    public EventCallback<QueryModel<TItem>> OnChange { get; set; }

    [Parameter]
    public int CurrentPage { get; set; } = 1;

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public bool Responsive { get; set; }
    
    [Parameter]
    public bool Loading { get; set; }

    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }
    
    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }

    protected virtual object GetColumnValue(object data, string fieldName)
    {
        return data.GetType().GetProperty(fieldName)?.GetValue(data);
    }
    
    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter.Invoke(item);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                convertedValue);
        }

        return convertedValue;
    }
}

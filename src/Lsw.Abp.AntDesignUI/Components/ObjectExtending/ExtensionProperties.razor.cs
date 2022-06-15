using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Lsw.Abp.AntDesignUI.Components.ObjectExtending;

public partial class ExtensionProperties<TEntityType, TResourceType> : ComponentBase
    where TEntityType : IHasExtraProperties
{
    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }

    [Parameter]
    public AbpBlazorMessageLocalizerHelper<TResourceType> LH { get; set; }

    [Parameter]
    public TEntityType Entity { get; set; }
    
    private RenderFragment ExtensionPropertyRender(ObjectExtensionPropertyInfo propertyInfo) => builder =>
    {
        var inputType = propertyInfo.GetInputType();
        builder.OpenComponent(0, inputType.MakeGenericType(typeof(TEntityType), typeof(TResourceType)));
        builder.AddAttribute(1, "PropertyInfo", propertyInfo);
        builder.AddAttribute(2, "Entity", Entity);
        builder.AddAttribute(3, "LH", LH);
        builder.CloseComponent();
    };
}

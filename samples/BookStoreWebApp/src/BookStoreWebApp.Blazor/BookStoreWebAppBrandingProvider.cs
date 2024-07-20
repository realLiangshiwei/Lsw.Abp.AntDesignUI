using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BookStoreWebApp.Blazor;

[Dependency(ReplaceServices = true)]
public class BookStoreWebAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BookStoreWebApp";
}

using BookStoreWebApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace BookStoreWebApp.Blazor.Client;

public abstract class BookStoreWebAppComponentBase : AbpComponentBase
{
    protected BookStoreWebAppComponentBase()
    {
        LocalizationResource = typeof(BookStoreWebAppResource);
    }
}

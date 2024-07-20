using BookStoreWebApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookStoreWebAppController : AbpControllerBase
{
    protected BookStoreWebAppController()
    {
        LocalizationResource = typeof(BookStoreWebAppResource);
    }
}

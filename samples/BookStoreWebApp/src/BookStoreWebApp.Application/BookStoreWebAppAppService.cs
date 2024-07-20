using System;
using System.Collections.Generic;
using System.Text;
using BookStoreWebApp.Localization;
using Volo.Abp.Application.Services;

namespace BookStoreWebApp;

/* Inherit your application services from this class.
 */
public abstract class BookStoreWebAppAppService : ApplicationService
{
    protected BookStoreWebAppAppService()
    {
        LocalizationResource = typeof(BookStoreWebAppResource);
    }
}

using Volo.Abp.Modularity;

namespace BookStoreWebApp;

/* Inherit from this class for your domain layer tests. */
public abstract class BookStoreWebAppDomainTestBase<TStartupModule> : BookStoreWebAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

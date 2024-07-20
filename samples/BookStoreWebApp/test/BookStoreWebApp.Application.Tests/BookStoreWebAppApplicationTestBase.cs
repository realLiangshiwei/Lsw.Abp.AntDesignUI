using Volo.Abp.Modularity;

namespace BookStoreWebApp;

public abstract class BookStoreWebAppApplicationTestBase<TStartupModule> : BookStoreWebAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

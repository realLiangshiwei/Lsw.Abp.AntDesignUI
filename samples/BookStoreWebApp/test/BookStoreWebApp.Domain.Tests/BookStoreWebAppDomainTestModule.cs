using Volo.Abp.Modularity;

namespace BookStoreWebApp;

[DependsOn(
    typeof(BookStoreWebAppDomainModule),
    typeof(BookStoreWebAppTestBaseModule)
)]
public class BookStoreWebAppDomainTestModule : AbpModule
{

}

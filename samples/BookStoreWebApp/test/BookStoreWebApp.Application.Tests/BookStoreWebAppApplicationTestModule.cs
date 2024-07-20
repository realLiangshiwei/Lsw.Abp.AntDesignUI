using Volo.Abp.Modularity;

namespace BookStoreWebApp;

[DependsOn(
    typeof(BookStoreWebAppApplicationModule),
    typeof(BookStoreWebAppDomainTestModule)
)]
public class BookStoreWebAppApplicationTestModule : AbpModule
{

}

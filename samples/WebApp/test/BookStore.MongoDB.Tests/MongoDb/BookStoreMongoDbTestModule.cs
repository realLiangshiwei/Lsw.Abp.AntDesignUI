using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace BookStore.MongoDB;

[DependsOn(
    typeof(BookStoreApplicationTestModule),
    typeof(BookStoreMongoDbModule)
)]
public class BookStoreMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = BookStoreMongoDbFixture.GetRandomConnectionString();
        });
    }
}

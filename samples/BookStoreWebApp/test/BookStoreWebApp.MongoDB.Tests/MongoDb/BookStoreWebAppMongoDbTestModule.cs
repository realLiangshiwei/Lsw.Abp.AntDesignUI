using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace BookStoreWebApp.MongoDB;

[DependsOn(
    typeof(BookStoreWebAppApplicationTestModule),
    typeof(BookStoreWebAppMongoDbModule)
)]
public class BookStoreWebAppMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = BookStoreWebAppMongoDbFixture.GetRandomConnectionString();
        });
    }
}

using BookStoreWebApp.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BookStoreWebApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BookStoreWebAppMongoDbModule),
    typeof(BookStoreWebAppApplicationContractsModule)
    )]
public class BookStoreWebAppDbMigratorModule : AbpModule
{
}

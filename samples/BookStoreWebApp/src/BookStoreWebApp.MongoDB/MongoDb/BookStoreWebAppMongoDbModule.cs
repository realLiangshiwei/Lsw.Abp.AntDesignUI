using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.BackgroundJobs.MongoDB;
using Volo.Abp.FeatureManagement.MongoDB;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.MongoDB;
using Volo.Abp.PermissionManagement.MongoDB;
using Volo.Abp.SettingManagement.MongoDB;
using Volo.Abp.TenantManagement.MongoDB;
using Volo.Abp.Uow;

namespace BookStoreWebApp.MongoDB;

[DependsOn(
    typeof(BookStoreWebAppDomainModule),
    typeof(AbpPermissionManagementMongoDbModule),
    typeof(AbpSettingManagementMongoDbModule),
    typeof(AbpIdentityMongoDbModule),
    typeof(AbpOpenIddictMongoDbModule),
    typeof(AbpBackgroundJobsMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(AbpTenantManagementMongoDbModule),
    typeof(AbpFeatureManagementMongoDbModule)
    )]
public class BookStoreWebAppMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<BookStoreWebAppMongoDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}

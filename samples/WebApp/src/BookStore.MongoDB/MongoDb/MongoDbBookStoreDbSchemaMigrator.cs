using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using BookStore.Data;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MongoDB;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Reflection;

namespace BookStore.MongoDB;

public class MongoDbBookStoreDbSchemaMigrator : IBookStoreDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public MongoDbBookStoreDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        var dbContexts = _serviceProvider.GetServices<IAbpMongoDbContext>();
        var connectionStringResolver = _serviceProvider.GetRequiredService<IConnectionStringResolver>();
        var configuration = _serviceProvider.GetRequiredService<IConfiguration>();

        if (_serviceProvider.GetRequiredService<ICurrentTenant>().IsAvailable)
        {
            dbContexts = dbContexts.Where(x => !x.GetType().IsDefined(typeof(IgnoreMultiTenancyAttribute)));
        }

        foreach (var dbContext in dbContexts)
        {
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName(dbContext.GetType());
            var connectionString = await connectionStringResolver.ResolveAsync(connectionStringName);
            if (connectionString.IsNullOrWhiteSpace())
            {
                connectionString = await connectionStringResolver.ResolveAsync();
            }
            if (connectionString.IsNullOrWhiteSpace())
            {
                connectionString = configuration.GetConnectionString("Default");
            }
            if (connectionString.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException(
                    $"Could not find a MongoDB connection string for '{connectionStringName}' or 'Default'.");
            }

            var mongoUrl = new MongoUrl(connectionString);
            var databaseName = mongoUrl.DatabaseName;
            var client = new MongoClient(mongoUrl);

            if (databaseName.IsNullOrWhiteSpace())
            {
                databaseName = ConnectionStringNameAttribute.GetConnStringName(dbContext.GetType());
            }

            (dbContext as AbpMongoDbContext)?.InitializeCollections(client.GetDatabase(databaseName));
        }
    }
}

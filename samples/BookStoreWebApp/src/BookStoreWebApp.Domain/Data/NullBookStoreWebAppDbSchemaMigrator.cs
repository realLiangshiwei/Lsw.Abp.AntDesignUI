using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BookStoreWebApp.Data;

/* This is used if database provider does't define
 * IBookStoreWebAppDbSchemaMigrator implementation.
 */
public class NullBookStoreWebAppDbSchemaMigrator : IBookStoreWebAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

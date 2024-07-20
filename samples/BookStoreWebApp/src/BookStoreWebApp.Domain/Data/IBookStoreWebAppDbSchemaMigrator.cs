using System.Threading.Tasks;

namespace BookStoreWebApp.Data;

public interface IBookStoreWebAppDbSchemaMigrator
{
    Task MigrateAsync();
}

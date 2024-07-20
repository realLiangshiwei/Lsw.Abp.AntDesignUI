using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BookStoreWebApp.Blazor.Client;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var application = await builder.AddApplicationAsync<BookStoreWebAppBlazorClientModule>(options =>
        {
            options.UseAutofac();
        });

        var host = builder.Build();

        await application.InitializeApplicationAsync(host.Services);

        await host.RunAsync();
    }
}

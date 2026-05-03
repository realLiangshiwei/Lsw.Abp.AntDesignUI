## ABP Blazor WebAssembly - AntDesign Theme

Use this guide for an ABP `blazor` WebAssembly application. The matching working sample is `samples/WebAppBlazorWebAssembly`.

![Advanced theme settings panel](img/theme-settings-panel.png)

## 1. Create The App

```bash
abp new BookStore -u blazor -t app
```

The paths below use the generated `BookStore` solution layout.

## 2. Add References

Add these references to `src/BookStore.Blazor.Client/BookStore.Blazor.Client.csproj`:

- `Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme`
- `Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI`
- `Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI`
- `Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI`
- `Lsw.Abp.FeatureManagement.Blazor.WebAssembly.AntDesignUI`
- `Lsw.Abp.AntDesignThemeManagement.Blazor.WebAssembly`

Add this reference to `src/BookStore.HttpApi.Host/BookStore.HttpApi.Host.csproj`:

- `Lsw.Abp.AntDesignThemeManagement.Application`

Add this reference to `src/BookStore.Blazor/BookStore.Blazor.csproj`:

- `Lsw.Abp.AspnetCore.Components.WebAssembly.AntDesignTheme.Bundling`

Use `ProjectReference` inside this repository, or the same package names when consuming NuGet packages.

Remove the old Blazorise-based Blazor packages from the same projects if they exist:

- `Blazorise.Bootstrap5`
- `Blazorise.Icons.FontAwesome`
- `Blazorise.Components`
- `Volo.Abp.AspNetCore.Components.WebAssembly.LeptonXLiteTheme`
- `Volo.Abp.AspNetCore.Components.WebAssembly.LeptonXLiteTheme.Bundling`
- `Volo.Abp.Identity.Blazor.WebAssembly`
- `Volo.Abp.TenantManagement.Blazor.WebAssembly`
- `Volo.Abp.SettingManagement.Blazor.WebAssembly`
- `Volo.Abp.FeatureManagement.Blazor.WebAssembly`

Do not remove `Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite` from the HTTP API host unless you also replace the MVC/account-page theme.

## 3. Update The WebAssembly Client

Open `src/BookStore.Blazor.Client/BookStoreBlazorClientModule.cs`.

In the existing `[DependsOn]`, use these AntDesign entries:

```csharp
typeof(AbpIdentityBlazorWebAssemblyAntDesignModule),
typeof(AbpSettingManagementBlazorWebAssemblyAntDesignModule),
typeof(AbpFeatureManagementBlazorWebAssemblyAntDesignModule),
typeof(AbpTenantManagementBlazorWebAssemblyAntDesignModule),
typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeModule),
typeof(AbpAntDesignThemeManagementBlazorWebAssemblyModule)
```

Configure the client router:

```csharp
private void ConfigureRouter(ServiceConfigurationContext context)
{
    Configure<AbpRouterOptions>(options =>
    {
        options.AppAssembly = typeof(BookStoreBlazorClientModule).Assembly;
    });
}
```

Remove the old Blazorise provider setup if it exists.

## 4. Update The HTTP API Host

Open `src/BookStore.HttpApi.Host/BookStoreHttpApiHostModule.cs`.

Add the theme management application module to `[DependsOn]`:

```csharp
typeof(AbpAntDesignThemeManagementApplicationModule)
```

Expose the theme management application service:

```csharp
private void ConfigureConventionalControllers()
{
    Configure<AbpAspNetCoreMvcOptions>(options =>
    {
        options.ConventionalControllers.Create(typeof(BookStoreApplicationModule).Assembly);
        options.ConventionalControllers.Create(typeof(AbpAntDesignThemeManagementApplicationModule).Assembly);
    });
}
```

## 5. Update The Blazor Host

Open `src/BookStore.Blazor/BookStoreBlazorModule.cs`.

Add the WebAssembly AntDesign bundling module to `[DependsOn]`:

```csharp
typeof(AbpAspNetCoreComponentsWebAssemblyAntDesignThemeBundlingModule)
```

## 6. Update Razor Files

Add these imports to `src/BookStore.Blazor.Client/_Imports.razor`:

```razor
@using AntDesign
@using Lsw.Abp.AntDesignUI
@using Lsw.Abp.AntDesignUI.Components
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Layout
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Bundling
```

Use the AntDesign layout in `src/BookStore.Blazor.Client/Routes.razor`:

```razor
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme
@using Volo.Abp.AspNetCore.Components.WebAssembly.WebApp

<Router AppAssembly="typeof(Program).Assembly" AdditionalAssemblies="WebAppAdditionalAssembliesHelper.GetAssemblies<BookStoreBlazorClientModule>()">
    <Found Context="routeData">
        <AuthorizeRouteView RouteData="routeData" DefaultLayout="typeof(DefaultLayout)">
            <NotAuthorized>
                <RedirectToLogin />
            </NotAuthorized>
        </AuthorizeRouteView>
    </Found>
</Router>
```

## 7. Build And Run

From the solution root:

```bash
dotnet build
```

To run this repository sample, start all three projects:

```bash
cd samples/WebAppBlazorWebAssembly
dotnet run --project .\src\BookStore.DbMigrator\
dotnet run --project .\src\BookStore.HttpApi.Host\
dotnet run --project .\src\BookStore.Blazor\
```

Open `https://localhost:44376`.

Log in with `admin` / `1q2w3E*`, then verify that the AntDesign layout and right-side theme settings panel are visible.

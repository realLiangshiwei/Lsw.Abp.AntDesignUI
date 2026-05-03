## ABP Blazor Server - AntDesign Theme

Use this guide for an ABP `blazor-server` application. The matching working sample is `samples/WebAppBlazorServer`.

![Advanced theme settings panel](img/theme-settings-panel.png)

## 1. Create The App

```bash
abp new BookStore -u blazor-server -t app
```

The paths below use the generated `BookStore` solution layout.

## 2. Add References

Add these references to `src/BookStore.Blazor/BookStore.Blazor.csproj`:

- `Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme`
- `Lsw.Abp.IdentityManagement.Blazor.Server.AntDesignUI`
- `Lsw.Abp.TenantManagement.Blazor.Server.AntDesignUI`
- `Lsw.Abp.SettingManagement.Blazor.Server.AntDesignUI`
- `Lsw.Abp.FeatureManagement.Blazor.Server.AntDesignUI`
- `Lsw.Abp.AntDesignThemeManagement.Blazor.Server`

Use `ProjectReference` inside this repository, or the same package names when consuming NuGet packages.

Remove the old Blazorise-based Blazor packages from `BookStore.Blazor.csproj` if they exist:

- `Blazorise.Bootstrap5`
- `Blazorise.Icons.FontAwesome`
- `Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme`
- `Volo.Abp.Identity.Blazor.Server`
- `Volo.Abp.TenantManagement.Blazor.Server`
- `Volo.Abp.SettingManagement.Blazor.Server`
- `Volo.Abp.FeatureManagement.Blazor.Server`

Do not remove `Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite` unless you also replace the MVC/account-page theme.

## 3. Update `BookStoreBlazorModule`

Open `src/BookStore.Blazor/BookStoreBlazorModule.cs`.

In the existing `[DependsOn]`, replace the generated Blazor UI module entries with these AntDesign entries:

```csharp
typeof(AbpIdentityBlazorServerAntDesignModule),
typeof(AbpTenantManagementBlazorServerAntDesignModule),
typeof(AbpAspNetCoreComponentsServerAntDesignThemeModule),
typeof(AbpFeatureManagementBlazorServerAntDesignModule),
typeof(AbpAntDesignThemeManagementBlazorServerModule),
typeof(AbpSettingManagementBlazorServerAntDesignModule)
```

Configure the AntDesign bundle, router, and theme management API:

```csharp
private void ConfigureBundles()
{
    Configure<AbpBundlingOptions>(options =>
    {
        options.StyleBundles.Configure(
            BlazorAntDesignThemeBundles.Styles.Global,
            bundle => { bundle.AddFiles("/global-styles.css"); }
        );
    });
}

private void ConfigureRouter(ServiceConfigurationContext context)
{
    Configure<AbpRouterOptions>(options =>
    {
        options.AppAssembly = typeof(BookStoreBlazorModule).Assembly;
    });
}

private void ConfigureAutoApiControllers()
{
    Configure<AbpAspNetCoreMvcOptions>(options =>
    {
        options.ConventionalControllers.Create(typeof(BookStoreApplicationModule).Assembly);
        options.ConventionalControllers.Create(typeof(AbpAntDesignThemeManagementApplicationModule).Assembly);
    });
}
```

Call these methods from `ConfigureServices`. Remove the old Blazorise provider setup if it exists.

## 4. Update Razor Files

Add these imports to `src/BookStore.Blazor/_Imports.razor`:

```razor
@using AntDesign
@using Lsw.Abp.AntDesignUI
@using Lsw.Abp.AntDesignUI.Components
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Layout
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Bundling
```

Use the AntDesign layout in `src/BookStore.Blazor/Components/Routes.razor`:

```razor
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Routing
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Themes.AntDesignTheme
@using Microsoft.Extensions.Options
@inject IOptions<AbpRouterOptions> RouterOptions

<Router AppAssembly="typeof(Program).Assembly" AdditionalAssemblies="RouterOptions.Value.AdditionalAssemblies">
    <Found Context="routeData">
        <AuthorizeRouteView RouteData="routeData" DefaultLayout="typeof(DefaultLayout)">
            <NotAuthorized>
                <RedirectToLogin />
            </NotAuthorized>
        </AuthorizeRouteView>
    </Found>
</Router>
```

Use the AntDesign bundles in `src/BookStore.Blazor/Components/App.razor`:

```razor
@using Lsw.Abp.AspnetCore.Components.Server.AntDesignTheme.Bundling

<AbpStyles BundleName="@BlazorAntDesignThemeBundles.Styles.Global" />

<Routes @rendermode="InteractiveServer" />

<AbpScripts BundleName="@BlazorAntDesignThemeBundles.Scripts.Global" />
```

## 5. Build And Run

From the solution root:

```bash
dotnet build
```

To run this repository sample:

```bash
cd samples/WebAppBlazorServer
dotnet run --project .\src\BookStore.DbMigrator\
dotnet run --project .\src\BookStore.Blazor\
```

Open `https://localhost:44322`.

Log in with `admin` / `1q2w3E*`, then verify that the AntDesign layout and right-side theme settings panel are visible.

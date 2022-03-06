# Lsw.Abp.AntDesignUI

**An Abp Blazor Theme based [Ant-Design-Blazor](https://github.com/ant-design-blazor/ant-design-blazor) !**

[![NuGet](https://img.shields.io/nuget/v/Lsw.Abp.AntDesignUI.svg)](https://www.nuget.org/packages/Lsw.Abp.AntDesignUI/)
[![NuGet](https://img.shields.io/nuget/dt/Lsw.Abp.AntDesignUI.svg)](https://www.nuget.org/packages/Lsw.Abp.AntDesignUI/)

## Samples

Check the [samples](/samples/BookStore/)

![1](img/1.png)
![2](img/2.png)

## Quick Start

The first step is to use ABP CLI to create a new project.

`abp new BookStore -u blazor`

> See the [ABP official documentation](https://docs.abp.io) to learn [ABP framework](https://github.com/abpframework/abp).

**Open `BookStore.Blazor.csproj` and replace with the following:**

```csharp
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <BlazorWebAssemblyLoadAllGlobalizationData>true</BlazorWebAssemblyLoadAllGlobalizationData>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="6.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="6.0.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Autofac.WebAssembly" Version="5.1.4" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Lsw.Abp.IdentityManagement.Blazor.WebAssembly.AntDesignUI" Version="0.1.0" />
    <PackageReference Include="Lsw.Abp.SettingManagement.Blazor.WebAssembly.AntDesignUI" Version="0.1.0" />
    <PackageReference Include="Lsw.Abp.TenantManagement.Blazor.WebAssembly.AntDesignUI" Version="0.1.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\BookStore.HttpApi.Client\BookStore.HttpApi.Client.csproj" />
  </ItemGroup>

</Project>

```

**Open `_Imports.razor` and add with the following:**

```csharp
@using AntDesign
@using Lsw.Abp.AntDesignUI
@using Lsw.Abp.AntDesignUI.Components
@using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Layout
```

**Open `BookStoreBlazorModule` make the following changes:**

* Remove the `ConfigureBlazorise` method
* Fix wrong using namespace
* Update module dependencies
    * For example, replace `AbpIdentityBlazorWebAssemblyModule` with `AbpIdentityBlazorWebAssemblyAntDesignModule`

**Open `BookStoreMenuContributor` to update icon:**

* `"fas fa-home"` to `IconType.Outline.Home`
* `"fa fa-cog"` to `IconType.Outline.Setting`

**Open `Index.razor` and replace with the following:**

```csharp
@page "/"
@inherits BookStoreComponentBase

<AbpPageHeader Title="Index"></AbpPageHeader>

<div class="page-content">
    <div style="text-align: center">
        
        <Alert Type="@AlertType.Success"
               Message="Success"
               Description=" Congratulations, BookStore is successfully running!"
               ShowIcon="true"/>

        <Divider/>

    </div>
</div>

```

Run the `dotnet build` & `abp bundle` command in the `BookStore.Blazor` folder.

That's all, enjoy your code :).

![3](img/3.png)

## Road map

Updating...
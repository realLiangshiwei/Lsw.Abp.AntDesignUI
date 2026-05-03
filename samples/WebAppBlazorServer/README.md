# BookStore Blazor Server Sample

This sample demonstrates the AntDesign theme in an ABP Blazor Server application.

## Prerequisites

- .NET SDK 10.0+
- Node.js 18 or newer
- MongoDB
- ABP CLI (`dotnet tool install -g Volo.Abp.Cli` if needed)

## Run

Install front-end libraries:

```bash
abp install-libs
```

Apply database migrations and seed data:

```bash
dotnet run --project .\src\BookStore.DbMigrator\
```

Start the Blazor Server app:

```bash
dotnet run --project .\src\BookStore.Blazor\
```

Open `https://localhost:44322`.

## Default Login

- Username: `admin`
- Password: `1q2w3E*`

## What To Verify

- The application uses the refactored AntDesign Pro-style layout.
- The sidebar is responsive and can collapse on smaller screens.
- Authenticated users can open the floating theme settings panel.
- Theme settings apply immediately without restarting the application.

## Theme Settings Management

This sample is configured with `AntDesignThemeManagement`.

Go to `Administration -> Settings -> Theme settings management` to control which sections appear in the user-facing panel:

- `Enable theme settings`
- `Page style setting`
- `Navigation mode`
- `Regional settings`
- `Other settings`

If all child options are disabled, `Enable theme settings` is automatically disabled.

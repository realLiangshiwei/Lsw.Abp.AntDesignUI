using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Lsw.Abp.FeatureManagement.Blazor.AntDesignUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.FeatureManagement;
using Volo.Abp.ObjectExtending;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Localization;

namespace Lsw.Abp.TenantManagement.Blazor.AntDesignUI.Pages;

public partial class TenantManagement
{
    protected const string FeatureProviderName = "T";

    protected bool HasManageFeaturesPermission;
    protected string ManageFeaturesPolicyName;

    protected FeatureManagementModal FeatureManagementModal;

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> TenantManagementTableColumns => TableColumns.Get<TenantManagement>();

    public TenantManagement()
    {
        LocalizationResource = typeof(AbpTenantManagementResource);
        ObjectMapperContext = typeof(AbpTenantManagementBlazorAntDesignModule);

        CreatePolicyName = TenantManagementPermissions.Tenants.Create;
        UpdatePolicyName = TenantManagementPermissions.Tenants.Update;
        DeletePolicyName = TenantManagementPermissions.Tenants.Delete;

        ManageFeaturesPolicyName = TenantManagementPermissions.Tenants.ManageFeatures;
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Menu:TenantManagement"]));
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Tenants"]));

        return base.SetBreadcrumbItemsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManageFeaturesPermission = await AuthorizationService.IsGrantedAsync(ManageFeaturesPolicyName);
    }

    protected override string GetDeleteConfirmationMessage(TenantDto entity)
    {
        return string.Format(L["TenantDeletionConfirmationMessage"], entity.Name);
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["ManageHostFeatures"],
            async () => await FeatureManagementModal.OpenAsync(FeatureProviderName),
            IconType.Outline.Setting,
            requiredPolicyName: FeatureManagementPermissions.ManageHostFeatures);

        Toolbar.AddButton(L["NewTenant"],
            OpenCreateModalAsync,
            IconType.Outline.Plus,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<TenantManagement>()
            .AddRange(new EntityAction[]
            {
                new EntityAction
                {
                    Text = L["Edit"],
                    Visible = (data) => HasUpdatePermission,
                    Clicked = async (data) => { await OpenEditModalAsync(data.As<TenantDto>()); }
                },
                new EntityAction
                {
                    Text = L["Features"],
                    Visible = (data) => HasManageFeaturesPermission,
                    Clicked = async (data) =>
                    {
                        var tenant = data.As<TenantDto>();
                        await FeatureManagementModal.OpenAsync(FeatureProviderName, tenant.Id.ToString());
                    }
                },
                new EntityAction
                {
                    Text = L["Delete"],
                    Visible = (data) => HasDeletePermission,
                    Clicked = async (data) => await DeleteEntityAsync(data.As<TenantDto>()),
                    ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<TenantDto>())
                }
            });

        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetTableColumnsAsync()
    {
        TenantManagementTableColumns
            .AddRange(new TableColumn[]
            {
                new TableColumn
                {
                    Title = L["TenantName"],
                    Data = nameof(TenantDto.Name),
                }
            });

        TenantManagementTableColumns.AddRange(GetExtensionTableColumns(
            TenantManagementModuleExtensionConsts.ModuleName,
            TenantManagementModuleExtensionConsts.EntityNames.Tenant));

        TenantManagementTableColumns.Add(new TableColumn
        {
            Title = L["Actions"],
            Actions = EntityActions.Get<TenantManagement>()
        });
        
        return base.SetTableColumnsAsync();
    }
}

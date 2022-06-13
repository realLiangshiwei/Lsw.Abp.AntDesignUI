using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Lsw.Abp.PermissionManagement.Blazor.AntDesignUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.ObjectExtending;

namespace Lsw.Abp.IdentityManagement.Blazor.AntDesignUI.Pages;

public partial class RoleManagement
{
    protected const string PermissionProviderName = "R";

    protected PermissionManagementModal PermissionManagementModal;

    protected bool HasManagePermissionsPermission { get; set; }

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> RoleManagementTableColumns => TableColumns.Get<RoleManagement>();
    
    public RoleManagement()
    {
        ObjectMapperContext = typeof(AbpIdentityBlazorAntDesignModule);
        LocalizationResource = typeof(IdentityResource);

        CreatePolicyName = IdentityPermissions.Roles.Create;
        UpdatePolicyName = IdentityPermissions.Roles.Update;
        DeletePolicyName = IdentityPermissions.Roles.Delete;
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Menu:IdentityManagement"]));
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Roles"]));

        return base.SetBreadcrumbItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<RoleManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) =>
                        {
                            await OpenEditModalAsync(data.As<IdentityRoleDto>());
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Permissions"],
                        Visible = (data) => HasManagePermissionsPermission,
                        Clicked = async (data) =>
                        {
                            await PermissionManagementModal.OpenAsync(PermissionProviderName,
                                data.As<IdentityRoleDto>().Name);
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<IdentityRoleDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<IdentityRoleDto>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetTableColumnsAsync()
    {
        RoleManagementTableColumns
            .AddRange(new TableColumn[]
            {
                new TableColumn
                    {
                        Title = L["RoleName"],
                        Data = nameof(IdentityRoleDto.Name),
                        Component = typeof(RoleNameComponent)
                    }
            });
        
        RoleManagementTableColumns.AddRange(GetExtensionTableColumns(IdentityModuleExtensionConsts.ModuleName,
            IdentityModuleExtensionConsts.EntityNames.Role));
        
        RoleManagementTableColumns.Add(new TableColumn
        {
            Title = L["Actions"],
            Actions = EntityActions.Get<RoleManagement>()
        });
        
        return base.SetTableColumnsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission =
            await AuthorizationService.IsGrantedAsync(IdentityPermissions.Roles.ManagePermissions);
    }

    protected override string GetDeleteConfirmationMessage(IdentityRoleDto entity)
    {
        return string.Format(L["RoleDeletionConfirmationMessage"], entity.Name);
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        
        Toolbar.AddButton(L["NewRole"],
            OpenCreateModalAsync,
            IconType.Outline.Plus,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }
}

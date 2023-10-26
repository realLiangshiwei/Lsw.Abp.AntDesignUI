using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class UserManagement
{
    protected const string PermissionProviderName = "U";

    protected PermissionManagementModal PermissionManagementModal;

    protected IReadOnlyList<IdentityRoleDto> Roles;

    protected AssignedRoleViewModel[] NewUserRoles;
    
    protected AssignedRoleViewModel[] EditUserRoles;

    protected bool HasManagePermissionsPermission { get; set; }

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> UserManagementTableColumns => TableColumns.Get<UserManagement>();
    
    public UserManagement()
    {
        ObjectMapperContext = typeof(AbpIdentityBlazorAntDesignModule);
        LocalizationResource = typeof(IdentityResource);

        CreatePolicyName = IdentityPermissions.Users.Create;
        UpdatePolicyName = IdentityPermissions.Users.Update;
        DeletePolicyName = IdentityPermissions.Users.Delete;
    }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            Roles = (await AppService.GetAssignableRolesAsync()).Items;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Menu:IdentityManagement"]));
        BreadcrumbItems.Add(new AbpBreadcrumbItem(L["Users"]));

        return base.SetBreadcrumbItemsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission =
            await AuthorizationService.IsGrantedAsync(IdentityPermissions.Users.ManagePermissions);
    }

    protected override Task OpenCreateModalAsync()
    {
        NewUserRoles = Roles.Select(x => new AssignedRoleViewModel
        {
            Name = x.Name,
            IsAssigned = x.IsDefault
        }).ToArray();

        return base.OpenCreateModalAsync();
    }

    protected override Task OnCreatingEntityAsync()
    {
        // apply roles before saving
        NewEntity.RoleNames = NewUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();

        return base.OnCreatingEntityAsync();
    }

    protected override async Task OpenEditModalAsync(IdentityUserDto entity)
    {
        try
        {
            var userRoleNames = (await AppService.GetRolesAsync(entity.Id)).Items.Select(r => r.Name).ToList();

            EditUserRoles = Roles.Select(x => new AssignedRoleViewModel
            {
                Name = x.Name,
                IsAssigned = userRoleNames.Contains(x.Name)
            }).ToArray();

            await base.OpenEditModalAsync(entity);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override Task OnUpdatingEntityAsync()
    {
        // apply roles before saving
        EditingEntity.RoleNames = EditUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();

        return base.OnUpdatingEntityAsync();
    }

    protected override string GetDeleteConfirmationMessage(IdentityUserDto entity)
    {
        return string.Format(L["UserDeletionConfirmationMessage"], entity.UserName);
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<UserManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => await OpenEditModalAsync(data.As<IdentityUserDto>())
                    },
                    new EntityAction
                    {
                        Text = L["Permissions"],
                        Visible = (data) => HasManagePermissionsPermission,
                        Clicked = async (data) =>
                        {
                            await PermissionManagementModal.OpenAsync(PermissionProviderName,
                                data.As<IdentityUserDto>().Id.ToString(),
                                data.As<IdentityUserDto>().UserName);
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<IdentityUserDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<IdentityUserDto>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetTableColumnsAsync()
    {
        UserManagementTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["UserName"],
                        Data = nameof(IdentityUserDto.UserName),
                    },
                    new TableColumn
                    {
                        Title = L["Email"],
                        Data = nameof(IdentityUserDto.Email),
                    },
                    new TableColumn
                    {
                        Title = L["PhoneNumber"],
                        Data = nameof(IdentityUserDto.PhoneNumber),
                    }
            });
        
        UserManagementTableColumns.AddRange(GetExtensionTableColumns(IdentityModuleExtensionConsts.ModuleName,
            IdentityModuleExtensionConsts.EntityNames.User));
        
        UserManagementTableColumns.Add(new TableColumn
        {
            Title = L["Actions"],
            Actions = EntityActions.Get<UserManagement>()
        });
        
        return base.SetEntityActionsAsync();
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["NewUser"], OpenCreateModalAsync,
            IconType.Outline.Plus,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }
}

public class AssignedRoleViewModel
{
    public string Name { get; set; }

    public bool IsAssigned { get; set; }
}

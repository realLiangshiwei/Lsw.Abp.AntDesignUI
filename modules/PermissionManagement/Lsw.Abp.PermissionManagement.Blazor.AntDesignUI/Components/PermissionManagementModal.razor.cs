using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Localization;

namespace Lsw.Abp.PermissionManagement.Blazor.AntDesignUI.Components;

public partial class PermissionManagementModal
{
    [Inject]
    protected IPermissionAppService PermissionAppService { get; set; }
    
    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    private string _providerName;
    private string _providerKey;
    private bool _visible;

    private string _entityDisplayName;
    private List<PermissionGroupDto> _groups;
    private List<PermissionGrantInfoDto> _disabledPermissions = new();
    private string _selectedTabName;

    public PermissionManagementModal()
    {
        LocalizationResource = typeof(AbpPermissionManagementResource);
    }
    
    private async Task SaveAsync(MouseEventArgs e)
    {
        try
        {
            var updateDto = new UpdatePermissionsDto
            {
                Permissions = _groups
                    .SelectMany(g => g.Permissions)
                    .Select(p => new UpdatePermissionDto { IsGranted = p.IsGranted, Name = p.Name })
                    .ToArray()
            };

            await PermissionAppService.UpdateAsync(_providerName, _providerKey, updateDto);

            await CurrentApplicationConfigurationCacheResetService.ResetAsync();

            _visible = false;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void ClosingModal(MouseEventArgs e)
    {
        _visible = false;
    } 
    
    public async Task OpenAsync(string providerName, string providerKey, string entityDisplayName = null)
    {
        try
        {
            _providerName = providerName;
            _providerKey = providerKey;

            var result = await PermissionAppService.GetAsync(_providerName, _providerKey);

            _entityDisplayName = entityDisplayName ?? result.EntityDisplayName;
            _groups = result.Groups;

            foreach (var permission in _groups.SelectMany(x => x.Permissions))
            {
                if (permission.IsGranted && permission.GrantedProviders.All(x => x.ProviderName != _providerName))
                {
                    _disabledPermissions.Add(permission);
                }
            }

            _selectedTabName = GetNormalizedGroupName(_groups.First().Name);
            
            _visible = true;
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    
    private string GetNormalizedGroupName(string name)
    {
        return "PermissionGroup_" + name.Replace(".", "_");
    }
    
    private void GrantAllChanged(bool value)
    {
        foreach (var groupDto in _groups)
        {
            foreach (var permission in groupDto.Permissions)
            {
                if (!IsDisabledPermission(permission))
                {
                    SetPermissionGrant(permission, value);
                }
            }
        }
    }
    
    private void GroupGrantAllChanged(bool value, PermissionGroupDto permissionGroup)
    {
        foreach (var permission in permissionGroup.Permissions)
        {
            if (!IsDisabledPermission(permission))
            {
                SetPermissionGrant(permission, value);
            }
        }
    }
    
    private bool IsDisabledPermission(PermissionGrantInfoDto permissionGrantInfo)
    {
        return _disabledPermissions.Any(x => x == permissionGrantInfo);
    }
    
    private void PermissionChanged(bool value, PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        SetPermissionGrant(permission, value);

         if (value && permission.ParentName != null)
         {
             var parentPermission = GetParentPermission(permissionGroup, permission);
        
             SetPermissionGrant(parentPermission, true);
         }
         else if (value == false)
         {
             var childPermissions = GetChildPermissions(permissionGroup, permission);
        
             foreach (var childPermission in childPermissions)
             {
                 SetPermissionGrant(childPermission, false);
             }
         }
    }

    private void SetPermissionGrant(PermissionGrantInfoDto permission, bool value)
    {
        if (permission.IsGranted == value)
        {
            return;
        }

        permission.IsGranted = value;
    }
    
    private PermissionGrantInfoDto GetParentPermission(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.First(x => x.Name == permission.ParentName);
    }

    private List<PermissionGrantInfoDto> GetChildPermissions(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.Where(x => x.Name.StartsWith(permission.Name)).ToList();
    }
    
    private string GetShownName(PermissionGrantInfoDto permissionGrantInfo)
    {
        if (!IsDisabledPermission(permissionGrantInfo))
        {
            return permissionGrantInfo.DisplayName;
        }

        return string.Format(
            "{0} ({1})",
            permissionGrantInfo.DisplayName,
            permissionGrantInfo.GrantedProviders
                .Where(p => p.ProviderName != _providerName)
                .Select(p => p.ProviderName)
                .JoinAsString(", ")
        );
    }
}

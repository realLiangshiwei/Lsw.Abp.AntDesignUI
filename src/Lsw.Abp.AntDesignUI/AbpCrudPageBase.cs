using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Lsw.Abp.AntDesignUI.Components;
using Lsw.Abp.AntDesignUI.Components.ObjectExtending;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;

namespace Lsw.Abp.AntDesignUI;

public abstract class AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        PagedAndSortedResultRequestDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey>
    where TEntityDto : class, IEntityDto<TKey>, new()
{
}

public abstract class AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TEntityDto>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput>
    where TEntityDto : class, IEntityDto<TKey>, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TCreateInput>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput>
    where TEntityDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    : AbpCrudPageBase<
        TAppService,
        TEntityDto,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TAppService : ICrudAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TEntityDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    : AbpCrudPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        TGetListOutputDto,
        TCreateInput,
        TUpdateInput>
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateInput : class, new()
    where TUpdateInput : class, new()
    where TGetListInput : new()
{
}

public abstract class AbpCrudPageBase<
        TAppService,
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        TListViewModel,
        TCreateViewModel,
        TUpdateViewModel>
    : AbpComponentBase
    where TAppService : ICrudAppService<
        TGetOutputDto,
        TGetListOutputDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TCreateInput : class
    where TUpdateInput : class
    where TGetListInput : new()
    where TListViewModel : IEntityDto<TKey>
    where TCreateViewModel : class, new()
    where TUpdateViewModel : class, new()
{
    [Inject] protected TAppService AppService { get; set; }

    [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

    protected virtual int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

    protected int CurrentPage = 1;
    protected string CurrentSorting;
    protected int TotalCount;
    protected bool Loading = false;
    protected TGetListInput GetListInput = new();
    protected IReadOnlyList<TListViewModel> Entities = Array.Empty<TListViewModel>();
    protected TCreateViewModel NewEntity;
    protected TKey EditingEntityId;
    protected TUpdateViewModel EditingEntity;
    protected Modal CreateModal;
    protected bool CreateModalVisible;
    protected Modal EditModal;
    protected bool EditModalVisible;
    protected Form<TCreateViewModel> CreateFormRef;
    protected Form<TUpdateViewModel> EditFormRef;
    protected List<AbpBreadcrumbItem> BreadcrumbItems = new();
    protected TableEntityActionsColumn<TListViewModel> EntityActionsColumn;
    protected EntityActionDictionary EntityActions { get; set; }
    protected TableColumnDictionary TableColumns { get; set; }

    protected string CreatePolicyName { get; set; }
    protected string UpdatePolicyName { get; set; }
    protected string DeletePolicyName { get; set; }

    public bool HasCreatePermission { get; set; }
    public bool HasUpdatePermission { get; set; }
    public bool HasDeletePermission { get; set; }

    protected AbpCrudPageBase()
    {
        NewEntity = new TCreateViewModel();
        EditingEntity = new TUpdateViewModel();
        TableColumns = new TableColumnDictionary();
        EntityActions = new EntityActionDictionary();
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await SetEntityActionsAsync();
        await SetTableColumnsAsync();
        await SetToolbarItemsAsync();
        await SetBreadcrumbItemsAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task SetPermissionsAsync()
    {
        if (CreatePolicyName != null)
        {
            HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
        }

        if (UpdatePolicyName != null)
        {
            HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
        }

        if (DeletePolicyName != null)
        {
            HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
        }
    }

    protected virtual async Task GetEntitiesAsync()
    {
        try
        {
            Loading = true;
            await UpdateGetListInputAsync();
            var result = await AppService.GetListAsync(GetListInput);
            Entities = MapToListViewModel(result.Items);
            TotalCount = (int)result.TotalCount;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
        
        Loading = false;
    }

    private IReadOnlyList<TListViewModel> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
    {
        if (typeof(TGetListOutputDto) == typeof(TListViewModel))
        {
            return dtos.As<IReadOnlyList<TListViewModel>>();
        }

        return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TListViewModel>>(dtos);
    }

    protected virtual Task UpdateGetListInputAsync()
    {
        if (GetListInput is ISortedResultRequest sortedResultRequestInput)
        {
            sortedResultRequestInput.Sorting = CurrentSorting;
        }

        if (GetListInput is IPagedResultRequest pagedResultRequestInput)
        {
            pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
        }

        if (GetListInput is ILimitedResultRequest limitedResultRequestInput)
        {
            limitedResultRequestInput.MaxResultCount = PageSize;
        }

        return Task.CompletedTask;
    }

    protected virtual async Task SearchEntitiesAsync()
    {
        CurrentPage = 1;

        await GetEntitiesAsync();

        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OnDataGridReadAsync(QueryModel<TListViewModel> e)
    {
        CurrentSorting = e.SortModel
            .Select(c => c.FieldName + (c.Sort == "descend" ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.PageIndex;

        await GetEntitiesAsync();

        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OpenCreateModalAsync()
    {
        try
        {
            CreateModalVisible = true;
            await CheckCreatePolicyAsync();

            NewEntity = new TCreateViewModel();

            await InvokeAsync(() =>
            {
                CreateFormRef?.Reset();
                StateHasChanged();

                return Task.CompletedTask;
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task CloseCreateModalAsync()
    {
        NewEntity = new TCreateViewModel();
        
        return InvokeAsync(() =>
        {
            CreateModalVisible = false;
        });
    }

    protected virtual Task ClosingCreateModal(MouseEventArgs  eventArgs)
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OpenEditModalAsync(TListViewModel entity)
    {
        try
        {
            EditModalVisible = true;
            
            await CheckUpdatePolicyAsync();

            var entityDto = await AppService.GetAsync(entity.Id);

            EditingEntityId = entity.Id;
            EditingEntity = MapToEditingEntity(entityDto);

            await InvokeAsync(() =>
            {
                EditFormRef?.Reset();
                StateHasChanged();
                return Task.CompletedTask;
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual TUpdateViewModel MapToEditingEntity(TGetOutputDto entityDto)
    {
        return ObjectMapper.Map<TGetOutputDto, TUpdateViewModel>(entityDto);
    }

    protected virtual TCreateInput MapToCreateInput(TCreateViewModel createViewModel)
    {
        if (typeof(TCreateInput) == typeof(TCreateViewModel))
        {
            return createViewModel.As<TCreateInput>();
        }

        return ObjectMapper.Map<TCreateViewModel, TCreateInput>(createViewModel);
    }

    protected virtual TUpdateInput MapToUpdateInput(TUpdateViewModel updateViewModel)
    {
        if (typeof(TUpdateInput) == typeof(TUpdateViewModel))
        {
            return updateViewModel.As<TUpdateInput>();
        }

        return ObjectMapper.Map<TUpdateViewModel, TUpdateInput>(updateViewModel);
    }

    protected virtual Task CloseEditModalAsync()
    {
        InvokeAsync(() =>
        {
            EditModalVisible = false;
        });
        return Task.CompletedTask;
    }

    protected virtual Task ClosingEditModal(MouseEventArgs eventArgs)
    {
        return Task.CompletedTask;
    }

    protected virtual async Task CreateEntityAsync()
    {
        try
        {
            var validate = true;
            if (CreateFormRef != null)
            {
                validate = CreateFormRef.Validate();
            }

            if (validate)
            {
                await OnCreatingEntityAsync();

                await CheckCreatePolicyAsync();
                var createInput = MapToCreateInput(NewEntity);
                await AppService.CreateAsync(createInput);

                await OnCreatedEntityAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnCreatingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnCreatedEntityAsync()
    {
        NewEntity = new TCreateViewModel();
        await GetEntitiesAsync();

        await InvokeAsync(() =>
        {
            CreateModalVisible = false;
        });
    }

    protected virtual async Task UpdateEntityAsync()
    {
        try
        {
            var validate = true;
            if (EditFormRef != null)
            {
                validate = EditFormRef.Validate();
            }

            if (validate)
            {
                await OnUpdatingEntityAsync();

                await CheckUpdatePolicyAsync();
                var updateInput = MapToUpdateInput(EditingEntity);
                await AppService.UpdateAsync(EditingEntityId, updateInput);

                await OnUpdatedEntityAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnUpdatingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnUpdatedEntityAsync()
    {
        await GetEntitiesAsync();

        await InvokeAsync(() =>
        {
            EditModalVisible = false;
        });
    }

    protected virtual async Task DeleteEntityAsync(TListViewModel entity)
    {
        try
        {
            await CheckDeletePolicyAsync();
            await OnDeletingEntityAsync();
            await AppService.DeleteAsync(entity.Id);
            await OnDeletedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual Task OnDeletingEntityAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnDeletedEntityAsync()
    {
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
        await Notify.Success(L["SuccessfullyDeleted"]);
    }

    protected virtual string GetDeleteConfirmationMessage(TListViewModel entity)
    {
        return UiLocalizer["ItemWillBeDeletedMessage"];
    }

    protected virtual async Task CheckCreatePolicyAsync()
    {
        await CheckPolicyAsync(CreatePolicyName);
    }

    protected virtual async Task CheckUpdatePolicyAsync()
    {
        await CheckPolicyAsync(UpdatePolicyName);
    }

    protected virtual async Task CheckDeletePolicyAsync()
    {
        await CheckPolicyAsync(DeletePolicyName);
    }

    /// <summary>
    /// Calls IAuthorizationService.CheckAsync for the given <paramref name="policyName"/>.
    /// Throws <see cref="AbpAuthorizationException"/> if given policy was not granted for the current user.
    ///
    /// Does nothing if <paramref name="policyName"/> is null or empty.
    /// </summary>
    /// <param name="policyName">A policy name to check</param>
    protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
    {
        if (string.IsNullOrEmpty(policyName))
        {
            return;
        }

        await AuthorizationService.CheckAsync(policyName);
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetEntityActionsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetTableColumnsAsync()
    {
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        return ValueTask.CompletedTask;
    }
    
    protected virtual IEnumerable<TableColumn> GetExtensionTableColumns(string moduleName, string entityType)
    {
        var properties = ModuleExtensionConfigurationHelper.GetPropertyConfigurations(moduleName, entityType);
        foreach (var propertyInfo in properties)
        {
            if (propertyInfo.IsAvailableToClients && propertyInfo.UI.OnTable.IsVisible)
            {
                if (propertyInfo.Name.EndsWith("_Text"))
                {
                    var lookupPropertyName = propertyInfo.Name.RemovePostFix("_Text");
                    var lookupPropertyDefinition = properties.SingleOrDefault(t => t.Name == lookupPropertyName);
                    yield return new TableColumn
                    {
                        Title = lookupPropertyDefinition.GetLocalizedDisplayName(StringLocalizerFactory),
                        Data = $"ExtraProperties[{propertyInfo.Name}]"
                    };
                }
                else
                {
                    var column = new TableColumn
                    {
                        Title = propertyInfo.GetLocalizedDisplayName(StringLocalizerFactory),
                        Data = $"ExtraProperties[{propertyInfo.Name}]"
                    };

                    if (propertyInfo.IsDate() || propertyInfo.IsDateTime())
                    {
                        column.DisplayFormat = propertyInfo.GetDateEditInputFormatOrNull();
                    }

                    if (propertyInfo.Type.IsEnum)
                    {
                        column.ValueConverter = (val) =>
                            EnumHelper.GetLocalizedMemberName(propertyInfo.Type, val.As<ExtensibleObject>().ExtraProperties[propertyInfo.Name], StringLocalizerFactory);
                    }

                    yield return column;
                }
            }
        }
    }
}



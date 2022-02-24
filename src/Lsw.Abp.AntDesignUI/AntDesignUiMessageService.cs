using System;
using System.Threading.Tasks;
using AntDesign;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AntDesignUI;

[Dependency(ReplaceServices = true)]
public class AntDesignUiMessageService : IUiMessageService , IScopedDependency
{
    [Inject]
    public ConfirmService ConfirmService { get; set; }
    
    [Inject]
    public ModalService ModalService { get; set; }
    
    private readonly IStringLocalizer<AbpUiResource> _localizer;

    public AntDesignUiMessageService(IStringLocalizer<AbpUiResource> localizer)
    {
        _localizer = localizer;
    }

    public async Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var uiMessageOptions = CreateDefaultOptions();
        options?.Invoke(uiMessageOptions);
        
        await ModalService.InfoAsync(CreateConfirmOptions(message, title ?? _localizer["Info"], uiMessageOptions, ConfirmButtons.OK));
    }

    public async Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var uiMessageOptions = CreateDefaultOptions();
        options?.Invoke(uiMessageOptions);

        await ModalService.SuccessAsync(CreateConfirmOptions(message, title ?? _localizer["Success"], uiMessageOptions, ConfirmButtons.OK));
    }

    public async Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var uiMessageOptions = CreateDefaultOptions();
        options?.Invoke(uiMessageOptions);

        await ModalService.WarningAsync(CreateConfirmOptions(message, title ?? _localizer["Warn"], uiMessageOptions, ConfirmButtons.YesNo));
    }

    public async Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var uiMessageOptions = CreateDefaultOptions();
        options?.Invoke(uiMessageOptions);

        await ModalService.ErrorAsync(CreateConfirmOptions(message, title ?? _localizer["Error"], uiMessageOptions, ConfirmButtons.YesNo));
    }

    public async Task<bool> Confirm(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        var uiMessageOptions = CreateDefaultOptions();
        options?.Invoke(uiMessageOptions);

        return await ModalService.ConfirmAsync(CreateConfirmOptions(message, title ?? _localizer["Confirm"], uiMessageOptions, ConfirmButtons.YesNo));
    }

    protected virtual ConfirmOptions CreateConfirmOptions(string message, string title, UiMessageOptions uiMessageOptions, ConfirmButtons confirmButtons)
    {
        var options = new ConfirmOptions
        {
            OkText = uiMessageOptions.ConfirmButtonText,
            CancelText = uiMessageOptions.CancelButtonText,
            Content = message,
            Title = title
        };

        if (confirmButtons == ConfirmButtons.YesNoCancel)
        {
            options.Button2Props.ChildContent = uiMessageOptions.OkButtonText;
            options.Button3Props.ChildContent = uiMessageOptions.CancelButtonText;
        }

        return options;
    }

    protected virtual UiMessageOptions CreateDefaultOptions()
    {
        return new UiMessageOptions
        {
            CenterMessage = true,
            ShowMessageIcon = true,
            OkButtonText = _localizer["Ok"],
            CancelButtonText = _localizer["Cancel"],
            ConfirmButtonText = _localizer["Yes"],
        };
    }
}

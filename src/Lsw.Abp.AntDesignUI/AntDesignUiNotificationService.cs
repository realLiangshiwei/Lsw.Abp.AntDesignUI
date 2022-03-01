using System;
using System.Threading.Tasks;
using AntDesign;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AntDesignUI;

[Dependency(ReplaceServices = true)]
public class AntDesignUiNotificationService: IUiNotificationService, IScopedDependency
{
    [Inject]
    public NotificationService NoticeService { get; set; }

    private readonly IStringLocalizer<AbpUiResource> _localizer;

    public AntDesignUiNotificationService(IStringLocalizer<AbpUiResource> localizer)
    {
        _localizer = localizer;
    }

    public async Task Info(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Info"], message, NotificationType.Info);
    }

    public async Task Success(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Success"], message, NotificationType.Success);
    }

    public async Task Warn(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Warn"], message, NotificationType.Warning);
    }

    public async Task Error(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Error"], message, NotificationType.Error);
    }

    protected virtual async Task Notify(string title, string message, NotificationType notificationType)
    {
        await NoticeService.Open(new NotificationConfig
        {
            Message = title,
            Description = message,
            Placement = NotificationPlacement.BottomRight,
            NotificationType = notificationType
        });
    }

    protected virtual UiNotificationOptions CreateDefaultOptions()
    {
        return new UiNotificationOptions();
    }
}

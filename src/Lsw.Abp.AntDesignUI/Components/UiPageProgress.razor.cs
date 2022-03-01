using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Progression;

namespace Lsw.Abp.AntDesignUI.Components;

public partial class UiPageProgress : ComponentBase
{
    private double _percent;

    private ProgressStatus _progressStatus;

    private string _css = "uiPageProgress";

    private Dictionary<string, string> _gradients = new()
    {
        { "0%", "#108ee9" },
        { "100%", "#87d068" }
    };

    [Inject] public IUiPageProgressService UiPageProgressService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        UiPageProgressService.ProgressChanged += OnProgressChanged;
    }

    protected virtual void OnProgressChanged(object sender, UiPageProgressEventArgs e)
    {
        _percent = e.Percentage ?? 0;
        SetProgressStatus(e.Options.Type);
        if (_percent != 0)
        {
            ShowProgress();
        }
        else
        {
            HideProgress();
        }
        
        StateHasChanged();
    }

    protected virtual void ShowProgress()
    {
        _css = "uiPageProgress uiPageProgress-show";
    }

    protected virtual void HideProgress()
    {
        _css = "uiPageProgress";
    }

    protected virtual void SetProgressStatus(UiPageProgressType pageProgressType)
    {
        _progressStatus = pageProgressType switch
        {
            UiPageProgressType.Info => ProgressStatus.Active,
            UiPageProgressType.Default => ProgressStatus.Active,
            UiPageProgressType.Success => ProgressStatus.Success,
            UiPageProgressType.Warning => ProgressStatus.Normal,
            UiPageProgressType.Error => ProgressStatus.Exception,
            _ => _progressStatus
        };
    }
}

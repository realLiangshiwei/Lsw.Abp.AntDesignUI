using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lsw.Abp.AntDesignUI;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Layout;

public class PageLayout : IScopedDependency, INotifyPropertyChanged
{
    private string? title;

    // TODO: Consider using this property for setting Page Title too.
    public virtual string? Title {
        get => title;
        set {
            title = value;
            OnPropertyChanged();
        }
    }

    private string? menuItemName;

    public string? MenuItemName {
        get => menuItemName;
        set
        {
            menuItemName = value;
            OnPropertyChanged();
        }
    }

    public virtual ObservableCollection<AbpBreadcrumbItem> BreadcrumbItems { get; } = new();

    public virtual ObservableCollection<AbpBreadcrumbItem> ToolbarItems { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public void Reset()
    {
        Title = string.Empty;
        MenuItemName = string.Empty;
        BreadcrumbItems.Clear();
        ToolbarItems.Clear();
    }
}
using System.Threading.Tasks;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}

using System.Threading.Tasks;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}

using System.Threading.Tasks;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}

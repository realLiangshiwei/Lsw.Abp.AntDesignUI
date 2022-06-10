using System.Threading.Tasks;
using Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;

namespace Lsw.Abp.AspnetCore.Components.Web.AntDesignTheme.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}

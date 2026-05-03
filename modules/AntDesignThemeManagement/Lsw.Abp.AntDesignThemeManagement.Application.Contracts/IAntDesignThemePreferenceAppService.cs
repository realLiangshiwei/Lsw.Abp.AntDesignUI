using System.Threading.Tasks;
using Lsw.Abp.AntDesignThemeManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Lsw.Abp.AntDesignThemeManagement;

public interface IAntDesignThemePreferenceAppService : IApplicationService
{
    Task<AntDesignThemePreferenceDto> GetAsync();

    Task UpdateAsync(UpdateAntDesignThemePreferenceDto input);

    Task UpdateThemeSettingsAvailabilityAsync(UpdateAntDesignThemeSettingsAvailabilityDto input);

    Task SetThemeSettingsEnabledAsync(bool isEnabled);
}

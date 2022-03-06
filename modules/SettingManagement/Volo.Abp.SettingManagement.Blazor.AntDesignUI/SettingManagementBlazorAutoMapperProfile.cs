using AutoMapper;

namespace Volo.Abp.SettingManagement.Blazor.AntDesignUI;

public class SettingManagementBlazorAutoMapperProfile : Profile
{
    public SettingManagementBlazorAutoMapperProfile()
    {
        CreateMap<EmailSettingsDto, UpdateEmailSettingsDto>();
    }
}

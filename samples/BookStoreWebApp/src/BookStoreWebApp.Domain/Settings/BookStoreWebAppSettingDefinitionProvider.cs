using Volo.Abp.Settings;

namespace BookStoreWebApp.Settings;

public class BookStoreWebAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BookStoreWebAppSettings.MySetting1));
    }
}

using Volo.Abp.UI.Navigation;

namespace AntDesignUIApp;

public class AntDesignUiAppMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "Home",
                    "Home",
                    "/",
                    icon: "home"
                )
            );

            var admin = new ApplicationMenuItem("Admin", "Admin"); 

            admin.AddItem(new ApplicationMenuItem("Users",
                "Users",
                "/Users"));
        
            admin.AddItem(new ApplicationMenuItem("Roles",
                "Roles",
                "/Roles"));

            context.Menu.AddItem(admin);
        }
        
        return Task.CompletedTask;
    }
}

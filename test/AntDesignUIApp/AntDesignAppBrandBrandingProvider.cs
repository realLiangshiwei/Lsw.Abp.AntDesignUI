using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AntDesignUIApp;

[Dependency(ReplaceServices = true)]
public class AntDesignAppBrandBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AntDesignApp";

    public override string LogoUrl => "logo.svg";
}

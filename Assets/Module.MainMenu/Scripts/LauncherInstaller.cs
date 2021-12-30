namespace Module.MainMenu.Scripts.Controllers
{
    public class LauncherInstaller : Core.Launchers.LauncherInstaller
    {
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponent>();
        }
    }
}
using Module.Core;

namespace Module.Game.Scripts
{
    public class LauncherInstaller: Core.Launchers.LauncherInstaller
    {
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponent>();
        }
    }
}
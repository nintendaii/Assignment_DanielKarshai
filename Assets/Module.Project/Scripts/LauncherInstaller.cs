using Module.Project.CommandSignals;
using Module.Project.Managers;
using UnityEngine;
using Zenject;

namespace Module.Project
{
    public class LauncherInstaller : Core.Launchers.LauncherInstaller
    {
        [SerializeField] private UnitAudioSfxController unitAudioSfxControllerPrefab;

        protected override void InstallServices()
        {
            Container.Bind<ManagerExternalCall>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<IInternalCall>().To<InternalCall>().AsSingle().NonLazy();
        }

        protected override void InstallCommandSignals()
        {
            Container.DeclareSignal<SignalShowMessageBox>();
            Container.BindSignal<SignalShowMessageBox>().ToMethod<CommandMessageBox>(signal => signal.Execute)
                .FromNew();

            Container.DeclareSignal<SignalHideMessageBox>();
            Container.BindSignal<SignalHideMessageBox>().ToMethod<CommandMessageBox>(signal => signal.Execute)
                .FromNew();
        }

        protected override void InstallFactory()
        {
            Container.BindFactory<UnitAudioSfxController, UnitAudioSfxControllerFactory>()
                .FromComponentInNewPrefab(unitAudioSfxControllerPrefab);
        }
    }
}
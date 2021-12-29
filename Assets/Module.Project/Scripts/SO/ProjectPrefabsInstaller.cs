using Module.Project.Controllers;
using Module.Project.Services;
using UnityEngine;
using Zenject;

namespace Module.Project.SO {
    [CreateAssetMenu(fileName = "ProjectPrefabsInstaller", menuName = "Installers/ProjectPrefabsInstaller")]
    public class ProjectPrefabsInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            SignalBusInstaller.Install(Container);
            Container.Bind<ServiceAudioMusic>().FromComponentInNewPrefabResource("Prefabs/Service.Audio.Music").AsSingle().NonLazy();
            Container.Bind<ServiceAudioSfx>().FromComponentInNewPrefabResource("Prefabs/Service.Audio.SFX").AsSingle().NonLazy();
            Container.Bind<ServiceTick>().FromComponentInNewPrefabResource("Prefabs/Service.Tick").AsSingle().NonLazy();
            Container.Bind<ProjectOverlayController>().FromComponentInNewPrefabResource("Prefabs/Project.Overlay").AsSingle().NonLazy();
            Container.Bind<DialogueController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<MessageBoxController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
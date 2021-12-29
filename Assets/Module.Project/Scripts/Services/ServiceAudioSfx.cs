using Module.Core.MVC;
using Module.Core.SO;
using UnityEngine;
using Zenject;

namespace Module.Project.Services {
    public class ServiceAudioSfx : ComponentControllerBase {
        // [Inject] private readonly UnitAudioSfxControllerFactory unitAudioSfxControllerFactory;
        //
        // private IObjectPool<UnitAudioSfxController> objectPool;
        //
        // public override void Initialize() {
        //     base.Initialize();
        //     objectPool = new ObjectPool<UnitAudioSfxController>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        // }
        //
        // public AudioSource Play(IAudioSfx audioSfx, float pitch = 1) {
        //     var audioSfxController = objectPool.Get();
        //     return audioSfxController.Play(audioSfx, () => { objectPool.Release(audioSfxController); }, pitch);
        // }
        //
        // private UnitAudioSfxController CreatePooledItem() {
        //     var unit = unitAudioSfxControllerFactory.Create();
        //     unit.Transform.SetParent(Transform);
        //     return unit;
        // }
        //
        // private static void OnReturnedToPool(UnitAudioSfxController unit) {
        //     unit.HideComponent();
        // }
        //
        // private static void OnTakeFromPool(UnitAudioSfxController unit) {
        //     unit.ShowComponent();
        // }
        //
        // private static void OnDestroyPoolObject(UnitAudioSfxController unit) {
        //     Destroy(unit.GameObject);
        // }
    }
}
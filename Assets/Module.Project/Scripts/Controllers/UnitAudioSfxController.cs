using System;
using System.Collections;
using Module.Core.MVC;
using Module.Core.SO;
using UnityEngine;
using Zenject;

namespace Module.Project
{
    [Serializable]
    public sealed class UnitAudioSfxView : ViewBase
    {
        [SerializeField] public AudioSource audioContent;
    }

    public sealed class UnitAudioSfxController : ComponentControllerBase<ModelBase, UnitAudioSfxView>
    {
        public AudioSource Play(IAudioSfx audioSfx, Action onComplete, float pitch = 1)
        {
            View.audioContent.pitch = pitch;
            StartCoroutine(PlayCoroutine(audioSfx, onComplete));
            return audioSfx != null ? View.audioContent : null;
        }

        private IEnumerator PlayCoroutine(IAudioSfx audioSfx, Action onComplete)
        {
            if (audioSfx != null)
            {
                View.audioContent.clip = audioSfx.GetAudioClip();
                View.audioContent.Play();
                yield return new WaitForSeconds(View.audioContent.clip.length);
            }

            onComplete?.Invoke();
        }
    }

    public class UnitAudioSfxControllerFactory : PlaceholderFactory<UnitAudioSfxController>
    {
    }
}
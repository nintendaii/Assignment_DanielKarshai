using System;
using System.Collections;
using Module.Core.MVC;
using Module.Core.SO;
using UnityEngine;

namespace Module.Project.Services
{
    [Serializable]
    public class AudioMusicView : ViewBase
    {
        [SerializeField] public AudioSource audioContent;
    }

    public class ServiceAudioMusic : ComponentControllerBase<ModelBase, AudioMusicView>
    {
        public AudioSource Play(IAudioMusic audioSfx)
        {
            if (audioSfx == null) return null;

            View.audioContent.clip = audioSfx.GetAudioClip();
            View.audioContent.loop = true;
            View.audioContent.Play();
            return View.audioContent;
        }

        public IEnumerator PlayCoroutine(IAudioMusic audioSfx)
        {
            var source = Play(audioSfx);
            if (source != null) yield return new WaitForSeconds(source.clip.length);
        }
    }
}
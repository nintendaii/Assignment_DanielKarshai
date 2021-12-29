using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Module.Core.SO {
    public interface IAudioSfx {
        AudioClip GetAudioClip();
    }
    
    [CreateAssetMenu(fileName = "AudioSfx", menuName = "Module/Common/AudioSfx")]
    public class SoAudioSfx : ScriptableObject {
        [SerializeField] private string key;
        [SerializeField] private AudioSfxData[] audioSfxDataArray = default;
        
        public string GetKey() => key;
        public IAudioSfx GetRandomData() => audioSfxDataArray?[Random.Range(0, audioSfxDataArray.Length)];
    }

    [Serializable]
    public class AudioSfxData : IAudioSfx {
        [SerializeField] private AudioClip audioClip;

        public AudioClip GetAudioClip() => audioClip;
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Module.Core.SO
{
    public interface IAudioMusic
    {
        AudioClip GetAudioClip();
    }

    [CreateAssetMenu(fileName = "AudioMusic", menuName = "Module/Common/AudioMusic")]
    public class SoAudioMusic : ScriptableObject
    {
        [SerializeField] private string key;
        [SerializeField] private AudioMusicData[] audioMusicDataArray = default;

        public string GetKey()
        {
            return key;
        }

        public IAudioMusic GetRandomData()
        {
            return audioMusicDataArray?[Random.Range(0, audioMusicDataArray.Length)];
        }
    }

    [Serializable]
    public class AudioMusicData : IAudioMusic
    {
        [SerializeField] private AudioClip audioClip;

        public AudioClip GetAudioClip()
        {
            return audioClip;
        }
    }
}
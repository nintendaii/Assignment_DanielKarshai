using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Module.Core.SO {
    [CreateAssetMenu(fileName = "_AudioMusicContainer", menuName = "Module/Common/AudioMusicContainer")]
    public class SoAudioMusicContainer : SoContainerBase {
        [SerializeField] private SoAudioMusic[] container;
        
        private readonly Dictionary<string, SoAudioMusic> valueByKey = new Dictionary<string, SoAudioMusic>();
        
        public IAudioMusic GetValue(string key) => !string.IsNullOrEmpty(key) && valueByKey.ContainsKey(key) ? valueByKey[key].GetRandomData() : null;
        
        public override void InstallBindings() {
            Container.BindInstance(this);
            
            foreach (var item in container) {
                valueByKey.Add(item.GetKey(), item);
            }
        }

        public override void Execute() {
#if UNITY_EDITOR
            var findAssets = AssetDatabase.FindAssets($"t:{typeof(SoAudioMusic)}", new[] { folderPath.GetPath });
            container = findAssets.Select(findAsset => AssetDatabase.LoadAssetAtPath<SoAudioMusic>(AssetDatabase.GUIDToAssetPath(findAsset))).ToArray();
            GenerateConstants();
#endif
        }

        private void GenerateConstants() {
            if (container == null) {
                return;
            }

            var sb = new StringBuilder();
            foreach (var dialogue in container) {
                sb.AppendLine($"public const string {dialogue.name.ToUpper().Replace('-', '_')} = \"{dialogue.name}\";");
            }
            Debug.Log(sb);
        }
    }
}
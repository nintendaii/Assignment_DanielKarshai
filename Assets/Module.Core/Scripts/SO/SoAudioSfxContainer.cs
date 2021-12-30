using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Module.Core.SO
{
    [CreateAssetMenu(fileName = "_AudioSfxContainer", menuName = "Module/Common/AudioSfxContainer")]
    public class SoAudioSfxContainer : SoContainerBase
    {
        [SerializeField] private SoAudioSfx[] container;

        private readonly Dictionary<string, SoAudioSfx> valueByKey = new Dictionary<string, SoAudioSfx>();

        public IAudioSfx GetValue(string key)
        {
            return !string.IsNullOrEmpty(key) && valueByKey.ContainsKey(key) ? valueByKey[key].GetRandomData() : null;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this);

            foreach (var item in container) valueByKey.Add(item.GetKey(), item);
        }

        public override void Execute()
        {
#if UNITY_EDITOR
            var findAssets = AssetDatabase.FindAssets($"t:{typeof(SoAudioSfx)}", new[] { folderPath.GetPath });
            container = findAssets.Select(findAsset =>
                AssetDatabase.LoadAssetAtPath<SoAudioSfx>(AssetDatabase.GUIDToAssetPath(findAsset))).ToArray();
            GenerateConstants();
#endif
        }

        private void GenerateConstants()
        {
            if (container == null) return;

            var sb = new StringBuilder();
            foreach (var dialogue in container)
                sb.AppendLine(
                    $"public const string {dialogue.name.ToUpper().Replace('-', '_')} = \"{dialogue.name}\";");
            Debug.Log(sb);
        }
    }
}
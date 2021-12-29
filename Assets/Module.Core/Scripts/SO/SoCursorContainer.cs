using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Module.Core.SO {
    [CreateAssetMenu(fileName = "_CursorContainer", menuName = "Module/Common/CursorContainer")]
    public class SoCursorContainer : SoContainerBase {
        [SerializeField] private SoCursor[] container;
        
        private readonly Dictionary<string, SoCursor> valueByKey = new Dictionary<string, SoCursor>();
        
        public ICursor GetValue(string key) => !string.IsNullOrEmpty(key) && valueByKey.ContainsKey(key) ? valueByKey[key].GetData() : null;
        
        public override void InstallBindings() {
            Container.BindInstance(this);
            
            foreach (var item in container) {
                var key = item.GetKey();
                if (valueByKey.ContainsKey(key)) {
                    valueByKey[key] = item;
                    Debug.LogWarning($"Duplicate key '{key}'");
                } else {
                    valueByKey.Add(item.GetKey(), item);
                }
            }
        }

        public override void Execute() {
#if UNITY_EDITOR
            var findAssets = AssetDatabase.FindAssets($"t:{typeof(SoCursor)}", new[] { folderPath.GetPath });
            container = findAssets.Select(findAsset => AssetDatabase.LoadAssetAtPath<SoCursor>(AssetDatabase.GUIDToAssetPath(findAsset))).ToArray();
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
using Module.Core.SO;
using UnityEditor;
using UnityEngine;

namespace Module.Core.Attributes {
    [CustomEditor(typeof(SoContainerBase), true)]
    public class SoContainerBaseEditor : Editor {
        public override void OnInspectorGUI() {
            var targetScript = (SoContainerBase) target;
            if (GUILayout.Button("Fill Container", GUILayout.Height(30))) {
                targetScript.Execute();
            }
            
            base.OnInspectorGUI();
        }
    }
}
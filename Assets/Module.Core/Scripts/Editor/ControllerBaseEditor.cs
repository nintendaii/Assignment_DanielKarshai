using UnityEditor;
using UnityEngine;

namespace Module.Core.Attributes {
    [CustomEditor(typeof(MVC.ControllerMonoBase),true)]
    public class ControllerBaseEditor : Editor {
        public override void OnInspectorGUI() {
            var origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.BoldAndItalic;
            EditorGUILayout.LabelField("namespace", target.GetType().Namespace, EditorStyles.label);
            EditorGUILayout.Space();
            EditorStyles.label.fontStyle = origFontStyle;
            base.OnInspectorGUI();
        }
    }
}
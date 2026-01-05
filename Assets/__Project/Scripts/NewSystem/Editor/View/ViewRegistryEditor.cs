using System.IO;
using System.Linq;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.Views.Base;
using UnityEditor;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Editor.View
{
    [CustomEditor(typeof(ViewRegistry))]
    public class ViewRegistryEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Автоматически найти все ViewBase-префабы"))
            {
                var registry = (ViewRegistry)target;
                var guids = AssetDatabase.FindAssets("t:Prefab");
                var views = guids
                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                    .Select(path => AssetDatabase.LoadAssetAtPath<GameObject>(path))
                    .Where(go => go != null && go.GetComponent<AViewBase>() != null)
                    .Select(go => go.GetComponent<AViewBase>())
                    .ToArray();

                Undo.RecordObject(registry, "Auto Fill View Prefabs");
                registry.entries = views;
                EditorUtility.SetDirty(registry);
                TDebug.Log($"[ViewRegistryEditor] Найдено и добавлено {views.Length} view-префабов.");
            }
        }
    }
}
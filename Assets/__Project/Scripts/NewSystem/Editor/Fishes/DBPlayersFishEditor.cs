using UnityEditor;
using UnityEngine;
using System.Linq;
using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.Fishes.Players;

namespace __Project.Scripts.NewSystem.Editor.Fishes
{
    [CustomEditor(typeof(DBPlayersFish), true)]
    public class DBPlayersFishEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Автоматически найти все PlayerFish-префабы"))
            {
                var registry = (DBPlayersFish)target;
                var guids = AssetDatabase.FindAssets("t:Prefab");
                var views = guids
                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                    .Select(path => AssetDatabase.LoadAssetAtPath<GameObject>(path))
                    .Where(go => go != null && go.GetComponent<PlayerFishBase>() != null)
                    .Select(go => go.GetComponent<PlayerFishBase>())
                    .ToArray();

                Undo.RecordObject(registry, "Auto Fill View Prefabs");
                registry.SetFishes(views.ToList());
                EditorUtility.SetDirty(registry);
                TDebug.Log($"[ViewRegistryEditor] Найдено и добавлено {views.Length} PlayerFish-префабов.");
            }
        }
    }
}
using System.Linq;
using __Project.Scripts.NewSystem.Database;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using UnityEditor;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Editor.Fishes
{
    [CustomEditor(typeof(DBEnemiesFish), true)]
    public class DBEnemiesFishEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Автоматически найти все EnemyFish-префабы"))
            {
                var registry = (DBEnemiesFish)target;
                var guids = AssetDatabase.FindAssets("t:Prefab");
                var views = guids
                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                    .Select(path => AssetDatabase.LoadAssetAtPath<GameObject>(path))
                    .Where(go => go != null && go.GetComponent<EnemyBase>() != null)
                    .Select(go => go.GetComponent<EnemyBase>())
                    .ToArray();

                Undo.RecordObject(registry, "Auto Fill View Prefabs");
                registry.SetFishes(views.ToList());
                EditorUtility.SetDirty(registry);
                TDebug.Log($"[ViewRegistryEditor] Найдено и добавлено {views.Length} EnemyFish-префабов.");
            }
        }
    }
}
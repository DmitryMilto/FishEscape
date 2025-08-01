using System.IO;
using __Project.Scripts.NewSystem.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.DataManager
{
    public class FileManager : IFileManager
    {
        private string GetPath(string fileName) =>
            Path.Combine(Application.persistentDataPath, fileName);

        public async UniTask SaveAsync<T>(string fileName, T data)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = GetPath(fileName);
            await UniTask.RunOnThreadPool(() => File.WriteAllText(path, json));
        }

        public async UniTask<T> LoadAsync<T>(string fileName) where T : class, new()
        {
            string path = GetPath(fileName);
            if (!File.Exists(path))
                return new T();
            string json = await UniTask.RunOnThreadPool(() => File.ReadAllText(path));
            return JsonUtility.FromJson<T>(json);
        }

        public bool Exists(string fileName) =>
            File.Exists(GetPath(fileName));
    }
}
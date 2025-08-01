using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Enums
{
    public interface IFileManager
    {
        UniTask SaveAsync<T>(string fileName, T data);
        UniTask<T> LoadAsync<T>(string fileName) where T : class, new();
        bool Exists(string fileName);
    }
}
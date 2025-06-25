using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Interfaces.View
{
    public interface IViewProvider
    {
        UniTask<T> OpenViewAsync<T>() where T : ViewBase;
        UniTask CloseViewAsync<T>(T view) where T : ViewBase;
        
        T OpenView<T>(T view) where T : ViewBase;
        void CloseView<T>(T view) where T : ViewBase;
        T GetView<T>() where T : ViewBase;
    }
}
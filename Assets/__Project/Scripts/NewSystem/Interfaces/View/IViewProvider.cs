using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Interfaces.View
{
    public interface IViewProvider
    {
        UniTask<T> OpenViewAsync<T>() where T : AViewBase;
        UniTask CloseViewAsync<T>(T view) where T : AViewBase;
        
        T OpenView<T>(T view) where T : AViewBase;
        void CloseView<T>(T view) where T : AViewBase;
        T GetView<T>() where T : AViewBase;
    }
}
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Interfaces.Animations
{
    public interface ISubViewAnimator
    {
        UniTask PlayOpenAsync();
        UniTask PlayCloseAsync();
        void PlayOpen();
        void PlayClose();
    }
}
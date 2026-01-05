using System.Threading;
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Interfaces.Animations
{
    public interface ISubViewAnimator
    {
        UniTask PlayOpenAsync(CancellationToken cancellationToken);
        UniTask PlayCloseAsync(CancellationToken cancellationToken);
        void PlayOpen();
        void PlayClose();
    }
}
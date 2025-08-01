using __Project.Scripts.NewSystem.Enums.Audios;
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Interfaces.Audios
{
    public interface IAudioService
    {
        void Play(SoundType type, SoundCategory? overrideCategory = null);
        void StopMusic();
        void SetSfxEnabled(bool enabled);
        void SetMusicEnabled(bool enabled);
        bool SfxEnabled { get; }
        bool MusicEnabled { get; }
    }
}
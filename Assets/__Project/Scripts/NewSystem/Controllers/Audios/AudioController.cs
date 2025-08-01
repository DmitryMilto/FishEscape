using System.Collections.Generic;
using System.Linq;
using System.Threading;
using __Project.Scripts.NewSystem.Database.Audios;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Interfaces.Audios;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.Controllers.Audios
{
    public class AudioController : MonoBehaviour, IAudioService
    {
        [SerializeField] private SoundDatabase database;
        [SerializeField] private AudioSource musicSource;
        private int sfxSourcesCount => 5;
        private float fadeDuration => 1.5f;

        private List<AudioSource> sfxSources;
        private bool sfxEnabled = true;
        private bool musicEnabled = true;
        private CancellationTokenSource musicCts;

        public bool SfxEnabled => sfxEnabled;
        public bool MusicEnabled => musicEnabled;

        [Inject]
        public void Construct(SoundDatabase database)
        {
            this.database = database;
        }

        private void Awake()
        {
            // Инициализация пула SFX AudioSource
            sfxSources = new List<AudioSource>(sfxSourcesCount);
            for (int i = 0; i < sfxSourcesCount; i++)
            {
                var src = gameObject.AddComponent<AudioSource>();
                src.playOnAwake = false;
                sfxSources.Add(src);
            }
        }

        public void Play(SoundType type, SoundCategory? overrideCategory = null)
        {
            var entry = database.GetEntry(type);
            if (entry == null) return;
            var category = overrideCategory ?? entry.category;
            if (category == SoundCategory.Sfx && sfxEnabled)
                PlaySfx(entry.clip);
            else if (category == SoundCategory.Music && musicEnabled)
                PlayMusic(type).Forget();
        }

        private void PlaySfx(AudioClip clip)
        {
            // Ищем свободный источник
            var source = sfxSources.FirstOrDefault(s => !s.isPlaying);
            if (source == null)
            {
                // Если все заняты — ищем тот, что уже закончил (time >= length)
                source = sfxSources.FirstOrDefault(s => s.time >= s.clip?.length);
                if (source == null)
                    source = sfxSources[0]; // fallback: заменяем первый
            }
            source.clip = clip;
            source.volume = 1f;
            source.Play();
        }

        private async UniTask PlayMusic(SoundType type)
        {
            var entry = database.GetEntry(type);
            if (entry == null || !musicEnabled) return;

            musicCts?.Cancel();
            musicCts = new CancellationTokenSource();
            var token = musicCts.Token;

            var oldSource = musicSource;
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = entry.clip;
            newSource.loop = true;
            newSource.volume = 0;
            newSource.Play();

            // Параллельно затухаем старый и наращиваем новый
            var fadeOut = oldSource.DOFade(0, fadeDuration).ToUniTask(cancellationToken: token);
            var fadeIn = newSource.DOFade(1f, fadeDuration).ToUniTask(cancellationToken: token);

            await UniTask.WhenAll(fadeOut, fadeIn);

            oldSource.Stop();
            Destroy(oldSource);
            musicSource = newSource;
        }

        public void StopMusic()
        {
            musicCts?.Cancel();
            musicSource.Stop();
        }

        public void SetSfxEnabled(bool enabled) => sfxEnabled = enabled;

        public void SetMusicEnabled(bool enabled)
        {
            musicEnabled = enabled;
            if (!enabled) StopMusic();
        }
    }
}
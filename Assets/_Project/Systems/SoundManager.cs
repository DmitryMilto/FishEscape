using UnityEngine;
using _Project.Core.Utils;

namespace _Project.Systems
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip);
            TDebug.Log($"[SoundManager] Playing SFX: {clip.name}");
        }

        public void PlayMusic(AudioClip music)
        {
            if (musicSource.clip == music) return;
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
            TDebug.Log($"[SoundManager] Playing music: {music.name}");
        }

        public void StopMusic()
        {
            musicSource.Stop();
            TDebug.Log("[SoundManager] Music stopped.");
        }
    }
}
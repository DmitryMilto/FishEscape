using System;
using UnityEngine;
using _Project.Core.Utils;

namespace _Project.Game.World
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private float levelDuration = 60f;

        public float TimeRemaining { get; private set; }
        public bool IsRunning { get; private set; }

        public event Action OnLevelStarted;
        public event Action OnLevelEnded;

        private void Update()
        {
            if (!IsRunning) return;

            TimeRemaining -= Time.deltaTime;

            if (TimeRemaining <= 0f)
            {
                EndLevel();
            }
        }

        public void StartLevel()
        {
            TimeRemaining = levelDuration;
            IsRunning = true;
            TDebug.Log("[LevelManager] Level started.");
            OnLevelStarted?.Invoke();
        }

        public void EndLevel()
        {
            if (!IsRunning) return;

            IsRunning = false;
            TimeRemaining = 0f;
            TDebug.Log("[LevelManager] Level ended.");
            OnLevelEnded?.Invoke();
        }

        public void StopLevel() => EndLevel();
    }
}
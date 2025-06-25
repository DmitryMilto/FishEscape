using System;

namespace __Project.Scripts.NewSystem.DataManager
{
    public static class GlobalEventsManager
    {
#if UNITY_EDITOR
        private static string _nameLog = $"<color=red>[{nameof(GlobalEventsManager)}]</color>";
#else
        private static string _nameLog = $"[{nameof(GlobalEventsManager)}]";
#endif
        
        //game processes events
        public static event Action OnDeath;
        public static event Action OnReplay;
        public static event Action<bool> OnPause;
        
        public static event Action OnAddLife;
        public static event Action OnRemoveLife;
        public static event Action<float> OnSpeedChange;


        public static void Death()
        {
            TDebug.Log($"{_nameLog}: Player has died.");
            OnDeath?.Invoke();
        }
        public static void Replay()
        {
            TDebug.Log($"{_nameLog}: Player has requested to replay.");
            OnReplay?.Invoke();
        }
        
        public static void AddLife()
        {
            TDebug.Log($"{_nameLog}: Player has gained a life.");
            OnAddLife?.Invoke();
        }
        public static void RemoveLife()
        {
            TDebug.Log($"{_nameLog}: Player has lost a life.");
            OnRemoveLife?.Invoke();
        }
        public static void SpeedChange(float speed)
        {
            TDebug.Log($"{_nameLog}: Player speed changed to {speed}.");
            OnSpeedChange?.Invoke(speed);
        }
        public static void PauseGame(bool isPaused)
        {
            TDebug.Log($"{_nameLog}: Game paused: {isPaused}");
            OnPause?.Invoke(isPaused);
        }
    }
}
#if ENABLE_TEST_LOG
#define ENABLE_LOGGING
#endif

using UnityEngine;

namespace _Project.Core.Utils
{
    public static class TDebug
    {
#if ENABLE_LOGGING
        public static void Log(object message) => Debug.Log(message);
        public static void LogWarning(object message) => Debug.LogWarning(message);
        public static void LogError(object message) => Debug.LogError(message);
#else
        public static void Log(object message) { }
        public static void LogWarning(object message) { }
        public static void LogError(object message) { }
#endif
    }
}
using UnityEngine;

public static class TDebug
{
    public static void Log(string message)
    {
#if ALL_DEBUG
        Debug.Log(message);
#endif
    }

    public static void LogError(string message)
    {
#if ALL_DEBUG
        Debug.LogError(message);
#endif
    }

    public static void LogWarning(string message)
    {
#if ALL_DEBUG
        Debug.LogWarning(message);
#endif
    }
}
using UnityEngine;
using _Project.Core.Utils;

namespace _Project.Systems
{
    public class TimeScaleManager : MonoBehaviour
    {
        public void SetNormal() => SetTime(1f);
        public void SetPaused() => SetTime(0f);
        public void SetFast(float factor) => SetTime(factor);

        private void SetTime(float scale)
        {
            Time.timeScale = scale;
            TDebug.Log($"[TimeScaleManager] Time scale set to: {scale}");
        }
    }
}
using UnityEngine;

namespace __Project.Scripts.NewSystem.Tools
{
    public static class ScreenBoundsUtils
    {
        public static float GetLeftScreenX()
        {
            if (Camera.main != null)
                return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
            else 
                return 0f;
        }
    
        public static float GetRightScreenX()
        {
            if (Camera.main != null)
                return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;
            else 
                return 0f;
        }
    }
}
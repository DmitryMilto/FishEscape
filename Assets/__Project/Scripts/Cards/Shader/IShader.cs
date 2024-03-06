using System.Collections;

namespace Scripts.Card
{
    public interface IShader
    {
        public void InitShader();
        public void InstanceShow();
        public void InstanceHide();
        public IEnumerator UpdateShader();
        public IEnumerator ResetShader();
        public void DisableShader();
        public void SetActive(bool value);
        public bool isActive();
    }
}
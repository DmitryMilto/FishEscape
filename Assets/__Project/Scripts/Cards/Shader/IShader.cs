
using System.Collections;
using UnityEngine;

namespace Scripts.Card
{
    public interface IShader
    {
        public void InitShader();
        public IEnumerator UpdateShader();
        public IEnumerator ResetShader();
        public void DisableShader();
        public void SetActive(bool value);
    }
}
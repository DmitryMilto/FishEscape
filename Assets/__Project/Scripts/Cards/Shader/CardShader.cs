using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Card
{
    public abstract class CardShader<T> : MonoBehaviour, IShader
    {
        public Image image { get; protected set; }

        [SerializeField]
        private string nameShader;
        public string NameShader { get => nameShader; }

        [SerializeField]
        private Material material;
        public Material Material { get => material; }

        [SerializeField]
        protected T value;

        [SerializeField]
        protected float EndValue;
        public void DisableShader()
        {
            Destroy(image.material);
        }
        public void InitShader()
        {
            image = GetComponent<Image>();
            var news = Instantiate(Material);
            image.material = news;
        }
        public abstract void InstanceShow();
        public abstract void InstanceHide();
        public abstract IEnumerator ResetShader();
        public abstract IEnumerator UpdateShader();
        public abstract bool isActive();

        public void SetActive(bool value)
        {
            this.gameObject.SetActive(value);
        }
    }
}
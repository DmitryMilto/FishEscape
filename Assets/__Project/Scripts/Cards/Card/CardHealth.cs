using System.Collections;
using UnityEngine;

namespace Scripts.Card
{
    public class CardHealth : MonoBehaviour
    {
        [SerializeReference]
        private IShader cardShader;
        public IShader CardShader
        {
            get
            {
                if (cardShader == null)
                {
                    cardShader = GetComponentInChildren<IShader>();
                }
                return cardShader;
            }
        }
        [SerializeField]
        private bool isActiveCard = true;
        public bool isActive
        {
            get => isActiveCard;
            private set => isActiveCard = value;
        }
        public void Damage()
        {
            StartCoroutine(AddDamage());
        }
        private IEnumerator AddDamage()
        {
            yield return CardShader.UpdateShader();
            isActive = false;
        }
        public void Health()
        {
            StartCoroutine(AddHealth());
        }
        private IEnumerator AddHealth()
        {
            yield return CardShader.ResetShader();
            isActive = true;
        }

        public void InstanceShow()
        {
            CardShader.InstanceShow();
            isActive = true;
        }
        public void InstanceHide()
        {
            CardShader.InstanceHide();
            isActive = false;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Scripts.Card
{
    public class CardHealth : MonoBehaviour
    {
        [SerializeField]
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
        private bool isactiveCard = true;
        public bool isActive
        {
            get => isactiveCard;
            private set => isactiveCard = value;
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
    }
}
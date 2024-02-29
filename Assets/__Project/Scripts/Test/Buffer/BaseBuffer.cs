using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Scripts.Buffer
{
    public abstract class BaseBuffer : MonoBehaviour
    {
        public abstract IBuffer buffer { get; }

        private IEnumerator coroutine;

        protected float time = 0.5f;
        private EnemyMono _enemy;
        public EnemyMono Enemy
        {
            get
            {
                if (_enemy == null) _enemy = GetComponent<EnemyMono>();
                return _enemy;
            }
        }
        private void OnEnable()
        {
            coroutine = buffer?.UpdateBuffer();
            StartCoroutine(coroutine);
        }
        private void OnDisable()
        {
            buffer?.ResetBuffer();
            this.transform.DOKill();
            StopCoroutine(coroutine);
            Destroy(this);
        }
    }
}
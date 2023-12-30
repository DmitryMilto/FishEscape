using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ChangeAlphaFish : MonoBehaviour
{

    private SpriteRenderer sprite => GetComponent<EnemyMono>().Fish;

    private float time = 0.5f;
    private IEnumerator coroutine;
    private void OnEnable()
    {
        coroutine = Scale();
        StartCoroutine(coroutine);
    }
    private void OnDisable()
    {
        this.transform.DOKill();
        StopCoroutine(coroutine);
        Destroy(this);
    }
    private IEnumerator Scale()
    {
        while (true)
        {
            SetFlashColor(0f);
            yield return new WaitForSeconds(time);
            SetFlashColor(1f);
            yield return new WaitForSeconds(time);
        }
    }

    private void SetFlashColor(float value)
    {
        sprite.DOFade(value, time).SetEase(Ease.Linear);
    }
}

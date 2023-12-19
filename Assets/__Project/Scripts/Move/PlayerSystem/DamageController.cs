using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private Camera cam;

    [Title("Damage")]
    [SerializeField] public bool isDamage;
    [Title("Other setting")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        if (cam == null) cam = Camera.main;
    }
    [SerializeField]
    private float time = 3f;
    private IEnumerator coroutine;
    [Button]
    public void RegisterDamage()
    {
        //spriteRenderer.DOFade(0f, 6f);
        if (isDamage)
        {
            coroutine = Damage();
            StartCoroutine(coroutine);
        }
        else
        {
            Debug.LogError($"Coroutine is not null");
        }
    }

    private IEnumerator Damage()
    {
        isDamage = false;
        float startTime = 0f;
        while (startTime < time)
        {
            SetFlashColor(0f);
            startTime += .1f;
            yield return new WaitForSeconds(.1f);
            SetFlashColor(1f);
            startTime += .1f;
            yield return new WaitForSeconds(.1f);
        }
        coroutine = null;
        isDamage = true;
    }
    private void SetFlashColor(float value)
    {
        spriteRenderer.DOFade(value, .1f).SetEase(Ease.Linear);
    }
}

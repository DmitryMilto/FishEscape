using DG.Tweening;
using Scripts.Buffer;
using System.Collections;
using UnityEngine;

public class ChangeAlphaFish : BaseBuffer, IBuffer
{
    public override IBuffer buffer => this;
    private SpriteRenderer sprite => Enemy.Fish;

    public IEnumerator UpdateBuffer()
    {
        while (true)
        {
            SetBuffer(0f);
            yield return new WaitForSeconds(time);
            SetBuffer(1f);
            yield return new WaitForSeconds(time);
        }
    }
    public void SetBuffer(float value)
    {
        sprite.DOFade(value, time).SetEase(Ease.Linear);
    }
    public void ResetBuffer()
    {
        sprite.DOFade(1f, 0.1f);
    }
}

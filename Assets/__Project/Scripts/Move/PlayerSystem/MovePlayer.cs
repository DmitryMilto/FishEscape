using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private InputManager manager;

    [ReadOnly]
    public float speed;

    [Inject]
    private SpeedSystem speedSystem;
    private void Move(Vector2 pos)
    {
        player.transform.DOKill();
        var news = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0f));
        var distance = Mathf.Abs(news.y - player.transform.position.y);
        player.transform.DOLocalMoveY(news.y, distance / speed).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
    }
    private void OnEnable()
    {
        manager.OnStartTouch += SwipeStart;
        manager.OnEndTouch += SwipeEnd;
        manager.OnUpdateTouch += SwipeUpdate;
        speedSystem.onSpeedEvent += SpeedUpdate;

        this.speed = speedSystem.speed;
    }
    private void OnDisable()
    {
        manager.OnStartTouch -= SwipeStart;
        manager.OnEndTouch -= SwipeEnd;
        manager.OnUpdateTouch -= SwipeUpdate;
        speedSystem.onSpeedEvent -= SpeedUpdate;
    }
    private void SpeedUpdate()
    {
        this.speed = speedSystem.speed;
    }

    private bool isMove = false;
    private void SwipeStart(bool value)
    {
        isMove = value;
    }
    private void SwipeEnd(bool value)
    {
        isMove = value;
        
    }
    private void SwipeUpdate(Vector2 position)
    {
        if (isMove)
        {
            Move(position);
        }
        else player.transform.DOKill();

    }
}

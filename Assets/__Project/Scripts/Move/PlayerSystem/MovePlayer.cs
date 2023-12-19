using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private InputManager manager;

    [ReadOnly]
    public float speed;
    private void Move(Vector2 pos)
    {
        player.transform.DOKill();
        var news = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));
        var distance = Vector3.Distance(player.transform.localPosition, news);
        player.transform.DOLocalMoveY(news.y, distance / speed).SetEase(Ease.Linear);
    }
    private void OnEnable()
    {
        manager.OnStartTouch += SwipeStart;
        manager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        manager.OnStartTouch -= SwipeStart;
        manager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {

    }
    private void SwipeEnd(Vector2 position, float time)
    {
        Move(position);
    }
}

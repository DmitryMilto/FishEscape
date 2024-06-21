using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class InitializeBubble : InitializeBase<EnumBonus>
{
    public BonusBubble bonusBubble { get; private set; }

    [SerializeField]
    private float speed = 6f;
    private ITypeMove move;

    [Button]
    public override void Initialize(EmptyDatabaseBonus<EnumBonus> bonus)
    {
        if (bonus != null)
        {
            bonusBubble = new BonusBubble(bonus.TypeBonus);
            bonusDB = bonus;

            this.Setting();

            move = new LineMove(this.speed);
        }

    }
    private void OnEnable()
    {
        this.transform.position = startPosition;
    }
    [SerializeField]
    private Camera cam => Camera.main;
    private float procent => cam.pixelHeight * 0.3f;
    Vector3 startPosition => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + 150f, UnityEngine.Random.Range(procent / 2, cam.pixelHeight - procent / 2), cam.nearClipPlane));
    Vector3 endPosition => (Vector2)cam.ScreenToWorldPoint(new Vector3(-12f, startPosition.y, cam.nearClipPlane));
    public override void Send()
    {
       this.bonusBubble.Send();
    }
    private void FixedUpdate()
    {
        if (this.transform.position.x > endPosition.x)
            this.transform.Translate(move.SpeedMove);
        else
            this.gameObject.SetActive(false);
    }
}

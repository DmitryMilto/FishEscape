using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class BaseBonus<T>:MonoBehaviour where T : Enum
{
    [SerializeField]
    private SpriteRenderer bubble;
    [SerializeField]
    private SpriteRenderer element;

    [SerializeField]
    [ReadOnly]
    protected T bonus;

    #region Abstract Method
    public abstract void Send();
    #endregion

    #region Virtual Method
    public virtual void Initialize(EmptyDatabaseBonus<T> database)
    {
        this.bonus = database.TypeBonus;
        Debug.Log("Finish Bonus");
        //bubble.enabled = database.TypeBubble == TypeBubble.WithoutBubble ? false : true;
        //element.sprite = database.element;
    }
    protected virtual void OnValidate()
    {
        this.name = bonus.ToString();
    }
    protected virtual void PlayDisable()
    {

    }
    #endregion

    #region Private Method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Send();
            this.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        PlayDisable();
    }
    #endregion
}

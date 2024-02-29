using Scripts.AutoSize;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase<T> : UIAutoSizeController
{
    #region Field
    public T View { get; private set; }

    #endregion
    #region Method
    protected override void Awake()
    {
        base.Awake();
        View = GetComponent<T>();
    }
    protected virtual void Start()
    {

    }

    protected void ShowEvent()
    {
        Debug.Log($"Show View - {this.name}");
        ShowCallback();
    }
    protected void HideEvent()
    {
        Debug.Log($"Hide View - {this.name}");
        HideCallback();
    }
    protected void VisableEvent()
    {
        Debug.Log($"Visable View - {this.name}");
        VisableCallback();
    }
    protected void HiddenEvent()
    {
        Debug.Log($"Hidden View - {this.name}");
        HiddenCallback();
    }
    #endregion

    #region Abstract Method
    protected abstract void ShowCallback();
    protected abstract void HideCallback();
    #endregion

    #region Virtual Method
    protected virtual void VisableCallback()
    {

    }
    protected virtual void HiddenCallback()
    {
    }
    #endregion

    protected void Clear(Transform transform)
    {
        Debug.Log($"Clear object {transform.name} - {this.name}");
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}

using Doozy.Runtime.UIManager.Containers;
using UnityEngine.Events;

public static class UIViewExtensions
{
    public static void AddListenerShow(this UIView view, UnityAction action)
    {
        view.OnShowCallback.Event.AddListener(action);
    }
    public static void AddListenerVisible(this UIView view, UnityAction action)
    {
        view.OnVisibleCallback.Event.AddListener(action);
    }
    public static void AddListenerHide(this UIView view, UnityAction action)
    {
        view.OnHideCallback.Event.AddListener(action);
    }
    public static void AddListenerHidden(this UIView view, UnityAction action)
    {
        view.OnHiddenCallback.Event.AddListener(action);
    }
}

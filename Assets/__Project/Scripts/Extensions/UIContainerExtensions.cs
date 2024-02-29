using Doozy.Runtime.UIManager.Containers;
using UnityEngine.Events;

public static class UIContainerExtensions
{
    public static void AddListenerShow(this UIContainer view, UnityAction action)
    {
        view.OnShowCallback.Event.AddListener(action);
    }
    public static void AddListenerVisible(this UIContainer view, UnityAction action)
    {
        view.OnVisibleCallback.Event.AddListener(action);
    }
    public static void AddListenerHide(this UIContainer view, UnityAction action)
    {
        view.OnHideCallback.Event.AddListener(action);
    }
    public static void AddListenerHidden(this UIContainer view, UnityAction action)
    {
        view.OnHiddenCallback.Event.AddListener(action);
    }
    public static void RemoveAllListeners(this UIContainer view)
    {
        view.OnHiddenCallback.Event.RemoveAllListeners();
        view.OnHideCallback.Event.RemoveAllListeners();
        view.OnVisibleCallback.Event.RemoveAllListeners();
        view.OnShowCallback.Event.RemoveAllListeners();
    }
}

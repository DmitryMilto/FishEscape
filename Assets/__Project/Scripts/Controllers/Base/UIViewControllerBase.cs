using Doozy.Runtime.UIManager.Containers;

public abstract class UIViewControllerBase : ControllerBase<UIView>
{
    protected override void Start()
    {
        base.Start();
        View.AddListenerShow(ShowEvent);
        View.AddListenerHide(HideEvent);

        View.AddListenerHidden(HiddenEvent);
        View.AddListenerVisible(VisableEvent);
    }
}

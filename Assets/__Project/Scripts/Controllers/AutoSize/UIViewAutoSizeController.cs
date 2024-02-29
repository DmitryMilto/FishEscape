
public class UIViewAutoSizeController : UIViewControllerBase
{
    protected override void HideCallback()
    {
    }

    protected override void ShowCallback()
    {
        AutoSizeWindow();
    }
}

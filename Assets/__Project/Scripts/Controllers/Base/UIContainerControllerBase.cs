using Doozy.Runtime.UIManager.Containers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIContainerControllerBase : ControllerBase<UIContainer>
{
    private void Start()
    {
        View.AddListenerShow(ShowEvent);
        View.AddListenerHide(HideEvent);

        View.AddListenerHidden(HiddenEvent);
        View.AddListenerVisible(VisableEvent);
    }
}

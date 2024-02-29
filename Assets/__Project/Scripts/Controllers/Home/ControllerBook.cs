using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerBook : UIViewControllerBase
{
    [SerializeField]
    private GameObject info;

    public PageInfo Collection;

    [SerializeField]
    private List<UIToggle> tabs;

    protected override void HideCallback()
    {
        ClosePageBook();
    }

    protected override void ShowCallback()
    {
        foreach(var tab in tabs)
        {
            tab.onClickEvent.AddListener(ClosePageBook);
        }
        AutoSizeWindow();
    }
    private void ClosePageBook()
    {
        Collection.OpeningBook(false);
    }
}

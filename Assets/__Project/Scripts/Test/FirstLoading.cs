using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLoading : MonoBehaviour
{
    [SerializeField] private Button _buttonMenu;
    // Start is called before the first frame update
    void Start()
    {
            //_buttonMenu.onClick.AddListener(OnClickLoadingMenu);
            //ActiveClick();
    }

    public void OnClickLoadingMenu()
    {
        Debug.Log("OnClick");
        //DeactiveButton();
    }
    private void ActiveClick()
    {
        _buttonMenu.transform.DOScale(1.05f, 2f).SetEase(Ease.Linear).OnComplete(DeactiveClick);
    }
    private void DeactiveClick()
    {
        _buttonMenu.transform.DOScale(1f, 2f).SetEase(Ease.Linear).OnComplete(ActiveClick);
    }
    private void DeactiveButton()
    {
        _buttonMenu.image.DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _buttonMenu.gameObject.SetActive(false);
            _buttonMenu.DOKill();
        });
    }
}

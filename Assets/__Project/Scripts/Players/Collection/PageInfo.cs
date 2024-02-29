using Doozy.Runtime.UIManager.Containers;
using FishEscape.Fishs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PageInfo : MonoBehaviour
{
    [Inject]
    private GameConfige gameConfige;
    [Title("Status Book")]
    [SerializeField]
    private UIContainer container;
    [Title("Image")]
    [SerializeField]
    private Image gameFish;

    [SerializeField]
    private Image realFish;

    [Title("Name")]
    [SerializeField]
    private TextMeshProUGUI nameFish;

    [SerializeField]
    private TextMeshProUGUI description;

    public void SetFish<T>(T info) where T : Fish
    {
        container.AddListenerVisible(() => SetInfoAboutFish(info));
        OpeningBook(true);
        
    }
    private void SetInfoAboutFish<T>(T info) where T : Fish
    {
        Debug.Log($"Show Page");
        gameFish.sprite = info.fish;
        realFish.sprite = info.RealPhotoFish;

        nameFish.text = info.fishName;
        description.text = info.Description;

        DefaultFish(info);
    }
    public void OpeningBook(bool isOpen)
    {
        if (isOpen)
        {
            if (container.isVisible || container.isShowing)
            {
                container.AddListenerHidden(() =>
                {
                    Debug.Log($"Hide Page");
                    container.Show();
                });
                container.Hide();
            }
            else
                container.Show();
        }
        else
        {
            container.RemoveAllListeners();
            container.Hide();
        }
    }
    private void DefaultFish<T>(T info) where T : Fish
    {
        if (info is PlayerFish)
            gameConfige.chooseFish = info as PlayerFish;
    }
}

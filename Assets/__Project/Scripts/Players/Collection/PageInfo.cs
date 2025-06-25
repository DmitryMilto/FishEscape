using FishEscape.Fishs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageInfo : MonoBehaviour
{
    private GameConfige gameConfige;
    [Title("Status Book")]
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
        OpeningBook(true);
    }
    private void SetInfoAboutFish<T>(T info) where T : Fish
    {
        gameFish.sprite = info.fish;
        realFish.sprite = info.RealPhotoFish;

        nameFish.text = info.fishName;
        description.text = info.Description;

        DefaultFish(info);
    }
    public void OpeningBook(bool isOpen)
    {

    }
    private void DefaultFish<T>(T info) where T : Fish
    {
        if (info is PlayerFish)
            gameConfige.chooseFish = info as PlayerFish;
    }
}

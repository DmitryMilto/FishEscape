using FishEscape.Fishs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageInfo : MonoBehaviour
{
    private GameConfige gameConfige;
    [SerializeField]
    private Image gameFish;

    [SerializeField]
    private Image realFish;

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
        gameFish.sprite = info.BookData.Illustration;
        realFish.sprite = info.BookData.RealPhoto;

        nameFish.text = info.FishName;
        description.text = info.BookData.Description;

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

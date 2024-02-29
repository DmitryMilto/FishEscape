using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CollectionBook
{
    [SerializeField]
    private Image gameFish;

    [SerializeField]
    private Image realFish;

    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI nameFish;

    [SerializeField]
    private TextMeshProUGUI descriptionFish;

    public void Clean()
    {
        //descriptionFish.text = nameFish.text = "";
        //background.sprite = realFish.sprite = gameFish.sprite = null;
    }
}

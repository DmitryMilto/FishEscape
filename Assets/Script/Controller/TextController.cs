using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Image langBttnImg;
    public Sprite[] flags;
    private int langIndex = 1;
    private string[] langArray = { "ru_RU", "en_EN" };

    [Header("Common Element")]
    public Text Company; //компания
    public Text Training; // свободная
    public Text Back; //назад
    public Text Warning; //предупреждение

    [Header("Main Element")]
    public Text SinglePlayerGame;  //одиночная игра
    public Text Setting; // настройки
    public Text SettingPanel; // настройки
    public Text Help; //подсказка
    public Text CompanyHelp; //компания
    public Text TrainingHelp; // свободная
    public Text CompanyDText; //подсказка на компанию
    public Text TrainingDText; //подсказка на свободную

    [Header("Selecton Element")]
    public Text[] nameLevel;
    public Text[] gameDifficulty;
    public Text[] danger;
    public Text[] record;
    public Text[] life;
    public Text difficultyLevelSelection;
    public Text playerChoice;
    public Text[] namePlayer;
    // Use this for initialization

    private string json;
    public Save lng = new Save();

    void Start()
    {
        for (int i = 0; i < langArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                langBttnImg.sprite = flags[i];
                break;
            }
        }
        Awake();
    }

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "ru_RU");
            else PlayerPrefs.SetString("Language", "en_US");
        }
        LangLoad();
    }

    private void LangLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#else
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");
#endif
        lng = JsonUtility.FromJson<Save>(json);
        TextLoad();
    }

    public void switchBttn()
    {
        if (langIndex != langArray.Length) langIndex++;
        else langIndex = 1;
        PlayerPrefs.SetString("Language", langArray[langIndex - 1]);
        langBttnImg.sprite = flags[langIndex - 1];
        LangLoad();
    }

    public void TextLoad()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            SinglePlayerGame.text = lng.SinglePlayerGame;  //одиночная игра
            Setting.text = lng.Setting; // настройки
            SettingPanel.text = lng.Setting; // настройки
            Help.text = lng.Help; //подсказка
            CompanyHelp.text = lng.Company; //компания
            TrainingHelp.text = lng.Training; // свободная
            CompanyDText.text = lng.CompanyDText; //подсказка на компанию
            TrainingDText.text = lng.TrainingDText; //подсказка на свободную

            Company.text = lng.Company; //компания
            Training.text = lng.Training; // свободная
            Back.text = lng.Back; //назад
        }
        if (SceneManager.GetActiveScene().name == "FreeScene")
        {
            CompanyController.lng = lng;
            difficultyLevelSelection.text = lng.difficultyLevelSelection;
            playerChoice.text = lng.playerChoice;
            Back.text = lng.Back; //назад
            Warning.text = lng.warningFree;

            for (int i = 0; i < gameDifficulty.Length; i++)
            {
                gameDifficulty[i].text = lng.gameDifficulty[i];
                danger[i].text = lng.danger;
                record[i].text = lng.record;
                life[i].text = lng.life;
            }
            for (int i = 0; i < nameLevel.Length; i++)
            {
                nameLevel[i].text = lng.nameLevel[i];
            }
            for(int i = 0; i<namePlayer.Length; i++)
            {
                namePlayer[i].text = lng.namePlayer[i];
            }
        }
        if(SceneManager.GetActiveScene().name == "LoadLevel")
        {
            LoadLevelController.lng = lng;
        }
    }

    ArgumentException: JSON parse error: Missing a name for object member.
UnityEngine.JsonUtility.FromJson[Save] (System.String json) (at C:/buildslave/unity/build/artifacts/generated/common/modules/JSONSerialize/JsonUtilityBindings.gen.cs:25)
TextController.LangLoad() (at Assets/Script/Controller/TextController.cs:78)
TextController.Awake() (at Assets/Script/Controller/TextController.cs:65)

}

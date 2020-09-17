using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CompanyController : MonoBehaviour {

    //------------------------------------
    [Header("Звезды на уровнях")]
    public GameObject[] starLevel;
    //------------------------------------
    [Header("Панель информации")]
    public GameObject panelInfoLevel;
    //------------------------------------
    [Header("Изображение уровней")]
    public Sprite[] imageLevel;
    public Image levelImage;
    //------------------------------------
    [Header("уровни")]
    public GameObject[] levelButton;
    public GameObject[] way;
    //------------------------------------
    [Header("Текст")]
    public Text nameLevel;
    public Text info;
    public Text company;
    public Text back;
    public Text play;
    //------------------------------------
    public static Save lng = new Save();
    private SaveProgress save = new SaveProgress();
    private FreeClass freeClass;
    private string path;

    // Use this for initialization
    void Start()
    {
        freeClass = new FreeClass();
        freeClass.mode = true;

#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "SaveProgress.json");
#else
        path = Path.Combine(Application.dataPath, "SaveProgress.json");
#endif
        if (File.Exists(path))
        {
            save = JsonUtility.FromJson<SaveProgress>(File.ReadAllText(path));
        }
        Awake();
    }

    void Awake()
    {
        panelInfoLevel.SetActive(false);
        closeLevel();

        back.text = lng.Back;
        company.text = lng.Company;
        play.text = lng.Play;
    }

    public void Level(int level)
    {
        freeClass.level = level;
        StarLevel(level);
        levelImage.sprite = imageLevel[level - 1];
        nameLevel.text = lng.nameLevel[level - 1];
        info.text = lng.Info[level - 1];
        panelInfoLevel.SetActive(true);

    }
    private void StarLevel(int level)
    {
        switch (level)
        {
            case 1: ActiveStar(save.CompanyLevel1); break;
            case 2: ActiveStar(save.CompanyLevel2); break;
            case 3: ActiveStar(save.CompanyLevel3); break;
            case 4: ActiveStar(save.CompanyLevel4); break;
            case 5: ActiveStar(save.CompanyLevel5); break;
        }
    }
    private void ActiveStar(int star)
    {
        starLevel[0].SetActive(false);
        starLevel[1].SetActive(false);
        starLevel[2].SetActive(false);

        for (int i = 0; i < star; i++)
        {
            starLevel[i].SetActive(true);
        }
    }
    private void closeLevel()
    {
        if (save.CompanyLevel1 >= 0)
            levelButton[0].SetActive(true);

        if (save.CompanyLevel1 > 0)
        {
            levelButton[1].SetActive(true);
            way[1].SetActive(true);
        }
        else {
            levelButton[1].SetActive(false);
            way[1].SetActive(false);
        }

        if (save.CompanyLevel2 > 0)
        {
            levelButton[2].SetActive(true);
            way[2].SetActive(true);
        }
        else {
            levelButton[2].SetActive(false);
            way[2].SetActive(false);
        }

        if (save.CompanyLevel3 > 0)
        {
            levelButton[3].SetActive(true);
            way[3].SetActive(true);
        }
        else {
            levelButton[3].SetActive(false);
            way[3].SetActive(false);
        }

        if (save.CompanyLevel4 > 0)
        {
            levelButton[4].SetActive(true);
            way[4].SetActive(true);
        }
        else {
            levelButton[4].SetActive(false);
            way[4].SetActive(false);
        }

    }

    public void Play()
    {
        freeClass.lifePlayer = 3;
        freeClass.time = 0.7f;
        freeClass.speedPlayer = 9f;
        LoadLevelController.save = freeClass;
        Application.LoadLevel("LoadLevel");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
    }

    public void Back()
    {
        if (panelInfoLevel.active)
        {
            panelInfoLevel.SetActive(false);
            return;
        }
        else
        {
            Application.LoadLevel("Main");
            return;
        }
    }
}

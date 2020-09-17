using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FreeController : MonoBehaviour {
    public GameObject SelectLevel;
    public GameObject SelectPlayer;
    public GameObject[] InfoLevelPanel;

    public GameObject[] level;
    public GameObject[] defficilty;

    public Text easy;
    public Text middle;
    public Text hard;

    private FreeClass free = new FreeClass();
    private SaveProgress save = new SaveProgress();
    private string nameSave = "CompanyLevel";
    private string path;

    private int[] life = { 3, 2, 4, 5, 5 };
    private float[] speedPlayer = { 10f, 15f, 5f, 10f, 15f };
    // Use this for initialization
    void Start () {
        SelectLevel.SetActive(false);
        SelectPlayer.SetActive(false);
        SetActiveInfo();

#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "SaveProgress.json");
#else
        path = Path.Combine(Application.dataPath, "SaveProgress.json");
#endif
        if (File.Exists(path))
        {
            save = JsonUtility.FromJson<SaveProgress>(File.ReadAllText(path));
        }
        ActiveLevel();
    }

    private void ActiveLevel()
    {
        if (save.CompanyLevel1 > 0)
        {
            level[0].SetActive(true);
        }
        else level[0].SetActive(false);
        if (save.CompanyLevel2 > 0)
        {
            level[1].SetActive(true);
        }
        else level[1].SetActive(false);
        if (save.CompanyLevel3 > 0)
        {
            level[2].SetActive(true);
        }
        else level[2].SetActive(false);
        if (save.CompanyLevel4 > 0)
        {
            level[3].SetActive(true);
        }
        else level[3].SetActive(false);
        if (save.CompanyLevel5 > 0)
        {
            level[4].SetActive(true);
        }
        else level[4].SetActive(false);
    }

    public void Level(int level)
    {
        free.level = level;
        SelectComplexity(level);
        SetActiveInfo(level);
        TextResultLevel(level);
        if (SelectPlayer.active)
            SelectPlayer.SetActive(false);
        SelectLevel.SetActive(true);
    }
    private void TextResultLevel(int level)
    {
        switch (level)
        {
            case 1: easy.text = save.level1.Easy.ToString();
                middle.text = save.level1.Middle.ToString();
                hard.text = save.level1.Hard.ToString();
                break;
            case 2:
                easy.text = save.level2.Easy.ToString();
                middle.text = save.level2.Middle.ToString();
                hard.text = save.level2.Hard.ToString();
                break;
            case 3:
                easy.text = save.level3.Easy.ToString();
                middle.text = save.level3.Middle.ToString();
                hard.text = save.level3.Hard.ToString();
                break;
            case 4:
                easy.text = save.level4.Easy.ToString();
                middle.text = save.level4.Middle.ToString();
                hard.text = save.level4.Hard.ToString();
                break;
            case 5:
                easy.text = save.level5.Easy.ToString();
                middle.text = save.level5.Middle.ToString();
                hard.text = save.level5.Hard.ToString();
                break;
        }
    }
    private void SelectComplexity(int level)
    {
        defficilty[0].SetActive(false);
        defficilty[1].SetActive(false);
        defficilty[2].SetActive(false);

        int star = 0;
        switch (level)
        {
            case 1: star = save.CompanyLevel1;
                break;
            case 2: star = save.CompanyLevel2;
                break;
            case 3: star = save.CompanyLevel3;
                break;
            case 4: star = save.CompanyLevel4;
                break;
            case 5: star = save.CompanyLevel5;
                break;
        }

        switch (star)
        {
            case 1:
                defficilty[0].SetActive(true);
                break;
            case 2:
                defficilty[0].SetActive(true);
                defficilty[1].SetActive(true);
                break;
            case 3:
                defficilty[0].SetActive(true);
                defficilty[1].SetActive(true);
                defficilty[2].SetActive(true);
                break;
            default:
                defficilty[0].SetActive(false);
                defficilty[1].SetActive(false);
                defficilty[2].SetActive(false);
                break;
        }
    }

    public void PanelSelectLevel(int complication)
    {
        free.time = free.SetTimeShark(complication);
        free.complication = complication;

        SelectLevel.SetActive(false);
        SelectPlayer.SetActive(true);
    }
    public void clickSelectPlayer(int position)
    {
        //free.namePlayer = name;
        free.mode = false;
        free.lifePlayer = life[position];
        free.speedPlayer = speedPlayer[position];
        LoadLevelController.save = free;
        Application.LoadLevel("LoadLevel");
    }

    public void Back()
    {
        if (SelectPlayer.active)
        {
            SelectPlayer.SetActive(false);
            SelectLevel.SetActive(true);
            return;
        }
        if (SelectLevel.active)
        {
            SelectLevel.SetActive(false);
            return;
        }
        if(!SelectLevel.active)
            Application.LoadLevel("Main");
    }

    private void SetActiveInfo(int active = 0)
    {
        for(int i = 0; i < InfoLevelPanel.Length; i++)
        {
            InfoLevelPanel[i].SetActive(false);
            if(i == active - 1)
            {
                InfoLevelPanel[i].SetActive(true);
            }
        }
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
}

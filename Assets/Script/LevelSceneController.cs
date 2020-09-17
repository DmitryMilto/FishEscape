using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneController : MonoBehaviour {

    public GameObject pausePanel;
    
    [Header("Панель конца в свободной")]
    public GameObject endPanel;

    [Header("Панель конца в компании")]
    public GameObject gameOverPanel;

    [Header("Панель победы в компании")]
    public GameObject victoryPanel;
    public GameObject[] star;

    [Header("Панель статуса")]
    public GameObject[] lifeImage;
    public GameObject statusFree;

    [Header("Картинки для сцены")]
    public Sprite[] imageLevel;
    public Image bgLevel;
    //-----------------------------------------------
    public static FreeClass freeClass;
    //-----------------------------------------------
    public static int xp = 0;

    [Header("Счет для свободной")]
    public Text scoreText;
    public Text resultValue;
    public static int score;
    //-----------------------------------------------
    private SaveProgress save = new SaveProgress();
    private string path;
    //-----------------------------------------------
    public static bool gameOver = false;
    public static bool pause = false;
    //-----------------------------------------------
    public Image image;
    private int[] enemy = { 10, 15, 20, 25, 30 };
    //-----------------------------------------------
    void Start()
    {
        Awake();
    }

    void Awake()
    {
        score = 0;
        gameOver = false;
        pause = false;

        //image.fillAmount = 0;

        JsonSave();
        xp = freeClass.lifePlayer;
        pausePanel.SetActive(pause);
        endPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        statusFree.SetActive(false);
        victoryPanel.SetActive(false);
        if (!freeClass.mode)
            statusFree.SetActive(true);

        PlayerCompany.speed = freeClass.speedPlayer;
        RandomEnemies.level = freeClass.level;
        RandomEnemies.time = freeClass.time;

        bgLevel.sprite = imageLevel[freeClass.level - 1];
    }

    private void ActiveLife(int life)
    {
        for (int i = 0; i < lifeImage.Length; i++)
            lifeImage[i].SetActive(false);

        for (int i = 0; i < life; i++)
            lifeImage[i].SetActive(true);
    }

    public void ScoreText(int score)
    {
        scoreText.text = (freeClass.complication * 10 * score).ToString();
    }

    void FixedUpdate()
    {
        if (xp < 0)
        {
            gameOver = true;
            endResult();
        }
        else ActiveLife(xp);

        if (!freeClass.mode)
            ScoreText(score);
        else
            OnVictory(score);
    }


    public float speed;
    public float setTimer = 0;

    void OnVictory(int score)
    {
        if (score == enemy[freeClass.level - 1])
        {
            onStar();
            SaveProgressResult();
            victoryPanel.SetActive(true);
            gameOver = true;
        }
    }
    void onStar()
    {
        star[0].SetActive(false);
        star[1].SetActive(false);
        star[2].SetActive(false);

        for (int i = 0; i < star.Length; i++)
        {
            if (i < xp)
            {
                star[i].SetActive(true);
            }
            else
                star[i].SetActive(false);
        }
    }
    private void JsonSave()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            path = Path.Combine(Application.persistentDataPath, "SaveProgress.json");
#else
        path = Path.Combine(Application.dataPath, "SaveProgress.json");
#endif
        if (File.Exists(path))
        {
            save = JsonUtility.FromJson<SaveProgress>(File.ReadAllText(path));
        }
    }

    private void endResult()
    {
        if (freeClass.mode)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            resultValue.text = (freeClass.complication * 10 * score).ToString();
            endPanel.SetActive(true);
        }
        SaveProgressResult();
    }

    private void SaveProgressResult()
    {
        if (!freeClass.mode)
        {
            if(freeClass.level == 1)
            {
                if(freeClass.complication == 1)
                {
                    save.level1.Easy = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 2)
                {
                    save.level1.Middle = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 3)
                {
                    save.level1.Hard = Convert.ToInt32(scoreText.text);
                }
            }
            if (freeClass.level == 2)
            {
                if (freeClass.complication == 1)
                {
                    save.level2.Easy = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 2)
                {
                    save.level2.Middle = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 3)
                {
                    save.level2.Hard = Convert.ToInt32(scoreText.text);
                }
            }
            if (freeClass.level == 3)
            {
                if (freeClass.complication == 1)
                {
                    save.level3.Easy = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 2)
                {
                    save.level3.Middle = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 3)
                {
                    save.level3.Hard = Convert.ToInt32(scoreText.text);
                }
            }
            if (freeClass.level == 4)
            {
                if (freeClass.complication == 1)
                {
                    save.level4.Easy = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 2)
                {
                    save.level4.Middle = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 3)
                {
                    save.level4.Hard = Convert.ToInt32(scoreText.text);
                }
            }
            if (freeClass.level == 5)
            {
                if (freeClass.complication == 1)
                {
                    save.level5.Easy = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 2)
                {
                    save.level5.Middle = Convert.ToInt32(scoreText.text);
                }
                if (freeClass.complication == 3)
                {
                    save.level5.Hard = Convert.ToInt32(scoreText.text);
                }
            }
        }
        else
        {
            switch (freeClass.level)
            {
                case 1:
                    if (save.CompanyLevel1 < xp)
                        save.CompanyLevel1 = xp;
                    break;
                case 2:
                    if (save.CompanyLevel2 < xp)
                        save.CompanyLevel2 = xp;
                    break;
                case 3:
                    if (save.CompanyLevel3 < xp)
                        save.CompanyLevel3 = xp;
                    break;
                case 4:
                    if (save.CompanyLevel4 < xp)
                        save.CompanyLevel4 = xp;
                    break;
                case 5:
                    if (save.CompanyLevel5 < xp)
                        save.CompanyLevel5 = xp;
                    break;
            }
        }
            File.WriteAllText(path, JsonUtility.ToJson(save));
    }
    //----------------------------------------
    //----------Готово-----------------------
    public void OnMain()
    {
        if (!freeClass.mode)
            SaveProgressResult();
        Application.LoadLevel("Main");
    }

    public void OnScene()
    {
        SelectScene.mode = freeClass.mode;
        Application.LoadLevel("FreeScene");
    }

    //public void Сontinue()
    //{
    //    pause = false;
    //    pausePanel.SetActive(pause);

    //}

    public void OpenPause()
    {
        pause = true;
        pausePanel.SetActive(pause);
    }

    public void OnReplay()
    {
        LevelSceneController.freeClass = freeClass;
        Application.LoadLevel("LevelScene");
    }
}

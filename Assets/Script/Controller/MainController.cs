using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
    public GameObject logo;
    public GameObject Home;
    public GameObject Player;

    public GameObject PanelSetting;
    public GameObject HelpSetting;


    bool Event = false;
    float setTimer = 0;

    void Awake()
    {
        Home.SetActive(true);
        Player.SetActive(false);
        PanelSetting.SetActive(false);
        HelpSetting.SetActive(false);

    }
    public void NewGame()
    {
        Home.SetActive(false);
        Player.SetActive(true);
    }
    public void Training()
    {
        SelectScene.mode = false;
        Application.LoadLevel("FreeScene");
    }
    public void Company()
    {
        SelectScene.mode = true;
        Application.LoadLevel("FreeScene");
    }
    public void Setting()
    {
        logo.SetActive(false);
        PanelSetting.SetActive(true);
    }
    public void Back()
    {
        Awake();
    }
    public void ClosePanel()
    {
        logo.SetActive(true);
        PanelSetting.SetActive(false);
    }
    public void Help()
    {
        setTimer = 0;
        Event = true;
        HelpSetting.SetActive(Event);
    }
    void Update()
    { 
        if (Event)
        {
            setTimer +=  Time.deltaTime;
            if (setTimer >= 2.5)
            {
                Event = false;
                HelpSetting.SetActive(Event);
            }
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Player.active)
                    Back();
                else
                    Application.Quit();
            }
        }
    }
}
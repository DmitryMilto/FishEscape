using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelController : MonoBehaviour
{

    public Sprite[] imageLevel;
    public Image bg;

    public Image loader;
    public static int level;

    public Text nameLevel;
    public Text nameMode;

    public static FreeClass save;
    public static Save lng;

    void Start()
    {
        bg.sprite = imageLevel[save.level - 1];
        nameLevel.text = lng.nameLevel[save.level - 1];

        if (save.mode)
        {
            nameMode.text = lng.Company;
        }
        else nameMode.text = lng.Training;
    }

    void FixedUpdate()
    {
        loader.transform.Rotate(Vector3.forward);
    }

    private float setTimer = 0;
    void Update()
    {
        setTimer += Time.deltaTime;
        if (setTimer >= 1.0)
        {
            LevelSceneController.freeClass = save;
            Application.LoadLevel("LevelScene");
        }
    }
}

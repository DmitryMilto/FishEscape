using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour {

    public GameObject company;
    public GameObject free;

    public static bool mode;

    void Start()
    {
        Awake();
    }
    void Awake()
    {
        if (mode)
        {
            company.SetActive(true);
            free.SetActive(false);
        }
        else
        {
            free.SetActive(true);
            company.SetActive(false);
        }
    }
}

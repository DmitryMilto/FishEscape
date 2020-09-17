using System;
using UnityEngine;

[Serializable]
public class Save {
    //Common
    public string Company; //компания
    public string Training; // свободная
    public string Back; //назад

    //Main
    public string SinglePlayerGame;  //одиночная игра
    public string Setting; // настройки
    public string Help; //подсказка
    public string CompanyDText; //подсказка на компанию
    public string TrainingDText; //подсказка на свободную

    //Company
    //public string[] level;
    public string[] nameLevel;
    public string warningCompany; //предупреждение
    public string[] Info;
    public string Play;

    //Selecton 
    public string[] gameDifficulty;
    public string danger;
    public string record;
    public string life;
    public string speed;
    public string difficultyLevelSelection;
    public string playerChoice;
    public string warningFree; //предупреждение
    public string[] namePlayer;
}

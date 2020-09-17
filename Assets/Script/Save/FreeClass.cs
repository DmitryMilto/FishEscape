

using UnityEngine;

public class FreeClass{

    public int level;
    public int complication; //уровень сложности
    public string namePlayer; //имя игрока
    public float speedPlayer; //скорость игрока
    public int lifePlayer; //жизни игрока
    public bool mode; //режим игры

    //public float speedShark;
    public float time;

    public float SetTimeShark(int compl)
    {
        if (compl == 1)
            return Random.Range(0.95f, 1.5f);
        if (compl == 2)
            return Random.Range(0.7f, 0.8f);
        if (compl == 3)
            return Random.Range(0.45f, 0.55f);
        return Random.Range(0.95f, 1.5f);
    }
}

using System;

[Serializable]
public class SaveProgress
{
    //Save Company
    public int CompanyLevel1;
    public int CompanyLevel2;
    public int CompanyLevel3;
    public int CompanyLevel4;
    public int CompanyLevel5;

    //Save Free
    public Level level1 = new Level();
    public Level level2 = new Level();
    public Level level3 = new Level();
    public Level level4 = new Level();
    public Level level5 = new Level();
}
[Serializable]
public class Level
{
    public int Easy;
    public int Middle;
    public int Hard;
}

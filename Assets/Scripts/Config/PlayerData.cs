using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    public int Happiness;
    public int Points;
    public HashSet<ClassAcao> SelectedActions;

    public PlayerData()
    {
        SelectedActions = new HashSet<ClassAcao>();
        Happiness = 100;
        Points = 0;
        Day = 1;
    }

    public PlayerData(SaveData saveData)
    {
        SelectedActions = new HashSet<ClassAcao>();
        Points = saveData.Points;
        Day = saveData.Day;
        Happiness = saveData.Happiness;

    }
}
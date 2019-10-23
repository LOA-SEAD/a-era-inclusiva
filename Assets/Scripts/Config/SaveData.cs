using System.Collections.Generic;

public class SaveData
{
    public int Day;
    public int Happiness;
    public string Name;
    public int Points;
    public HashSet<ClassAcao> SelectedActions;

    public SaveData(string name, PlayerData data)
    {
        Name = name;
        Day = 1;
        Happiness = data.Happiness;
        Points = data.Points;
        SelectedActions = data.SelectedActions;
    }


    public SaveData(string name, int day, int happiness, int points, HashSet<ClassAcao> selectedActions)
    {
        Name = name;
        Day = day;
        Happiness = happiness;
        Points = points;
        SelectedActions = selectedActions;
    }

}
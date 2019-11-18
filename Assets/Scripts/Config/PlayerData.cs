using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    private int _happiness;
    public int Happiness
    {
        get => _happiness;
        set
        {
            if (value > 100)
            {
                _happiness = 100;
            }
            else if (value < 0)
            {
                _happiness = 0;
            }
            else
            {
                _happiness = value;
            }
        }
    }

    public int Points;
    public HashSet<ClassAcao> SelectedActions;
    public List<string> Dialogs;

    public PlayerData()
    {
        SelectedActions = new HashSet<ClassAcao>();
        Happiness = 100;
        Points = 0;
        Day = 1;
        Dialogs = new List<string>();
    }

    public PlayerData(SaveData saveData)
    {
        SelectedActions = new HashSet<ClassAcao>();
        Points = saveData.Points;
        Day = saveData.Day;
        Happiness = saveData.Happiness;
        Dialogs = saveData.Dialogs;

    }
}
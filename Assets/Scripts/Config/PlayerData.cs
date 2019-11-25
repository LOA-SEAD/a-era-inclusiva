using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    public int Happiness;
    public int Points;
    public HashSet<ClassAcao> SelectedActions;
    public List<string> Dialogs;
    public int SelectedAvatar;
    //public Dictionary<string, int> staticImages;

    public PlayerData()
    {
        SelectedActions = new HashSet<ClassAcao>();
        Happiness = 100;
        Points = 0;
        Day = 1;
        Dialogs = new List<string>();
        SelectedAvatar = 0;
        //staticImages = new Dictionary<string, int>();
    }

    public PlayerData(SaveData saveData)
    {
        SelectedActions = new HashSet<ClassAcao>();
        Points = saveData.Points;
        Day = saveData.Day;
        Happiness = saveData.Happiness;
        Dialogs = saveData.Dialogs;
        SelectedAvatar = saveData.SelectedAvatar;
    }
}
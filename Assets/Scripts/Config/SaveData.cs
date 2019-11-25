using System.Collections.Generic;

public class SaveData
{
    public int Day;
    public int Happiness;
    public string Name;
    public int Points;
    public List<string> Dialogs;
    public HashSet<ClassAcao> SelectedActions;
    public int SelectedAvatar;

    public SaveData(string name, PlayerData data)
    {
        Name = name;
        Day = 1;
        Happiness = data.Happiness;
        Points = data.Points;
        SelectedActions = data.SelectedActions;
        Dialogs = data.Dialogs;
        SelectedAvatar = data.SelectedAvatar;
    }


    public SaveData(string name, int day, int happiness, int points, HashSet<ClassAcao> selectedActions, int selectedAvatar)
    {
        Name = name;
        Day = day;
        Happiness = happiness;
        Points = points;
        SelectedActions = selectedActions;
        SelectedAvatar = selectedAvatar;
    }

}
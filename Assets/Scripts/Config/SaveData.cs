using System.Collections.Generic;

[System.Serializable]
public class ResolucaoSave
{
    public int DemandIndex;
    public int ActionId;

    public ResolucaoSave(int demandIndex, int actionId)
    {
        DemandIndex = demandIndex;
        ActionId = actionId;
    }
}

public class SaveData
{
    public int Day;
    public int Happiness;
    public string Name;
    public int Points;
    public List<string> Dialogs;
    public List<int> SelectedMethodologyIds;
    public List<ResolucaoSave> SelectedResolutions;
    public int SelectedAvatar;
    public bool AulaConcluida;

    public SaveData(string name, PlayerData data)
    {
        Name = name;
        Day = data.Day;
        Happiness = data.Happiness;
        Points = data.Points;
        Dialogs = data.Dialogs;
        SelectedAvatar = data.SelectedAvatar;
        AulaConcluida = data.AulaConcluida;
        SelectedMethodologyIds = data.SelectedMethodologyIds;
        SelectedResolutions = data.SelectedResolutions;
    }

    public SaveData(
        string name,
        int day,
        int happiness,
        int points,
        List<string> dialogs,
        List<int> selectedMethodologyIds,
        List<ResolucaoSave> selectedResolutions,
        int selectedAvatar
    )
    {
        Name = name;
        Day = day;
        Happiness = happiness;
        Points = points;
        Dialogs = dialogs;
        SelectedMethodologyIds = selectedMethodologyIds;
        SelectedResolutions = selectedResolutions;
        SelectedAvatar = selectedAvatar;
    }
}

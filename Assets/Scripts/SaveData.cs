public class SaveData
{
    public int Day;
    public int Happiness;
    public string Name;
    public int Points;

    public SaveData(string name, SoundManager soundManager, PlayerData data)
    {
        Name = name;
        Day = 1;
        Happiness = data.Happiness;
        Points = data.Points;
    }


    public SaveData(string name, int day, int happiness, int points)
    {
        Name = name;
        Day = day;
        Happiness = happiness;
        Points = points;
    }
}
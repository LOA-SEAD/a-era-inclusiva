public class PlayerData
{
    public int Day;
    public int Happiness;
    public int Points;

    public PlayerData(int happiness, int points, int day)
    {
        Happiness = 100;
        Points = 0;
        Day = 1;
    }

    public PlayerData(SaveData saveData)
    {
        Points = saveData.Points;
        Day = saveData.Day;
        Happiness = saveData.Happiness;
    }
}
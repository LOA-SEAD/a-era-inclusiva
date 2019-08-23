
    public class PlayerData
    {
       

    public int Day;
    public int Points;
    public int Happiness;

        public PlayerData()
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

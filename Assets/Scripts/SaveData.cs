using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SaveData
{
    public string Name;
    public int Day;
    public int Happiness;
    public int Points;

    public SaveData(string name, GraphicsManager graphicsManager, SoundManager soundManager, PlayerData data)
    {
        Name = name;
        Day = 1;
        Happiness = data.Happiness;
        Points = data.Points;

    }


    public SaveData(string name, int day, string quality, Resolution resolution, int happiness, int points, float backgroundVol, float effectsVol, float voicesVol, bool fullscreen)
    {
        Name = name;
        Day = day;
        Happiness = happiness;
        Points = points;

    }
}
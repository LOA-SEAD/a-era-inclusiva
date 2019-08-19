using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SaveData
{
    public string Name;
    public int Day;
    public string Quality;
    public Resolution Resolution;
    public float Happiness;
    public int Points;
    public float BackgroundVol;
    public float EffectsVol;
    public float VoicesVol;

    public SaveData(string name, GraphicsManager graphicsManager, SoundManager soundManager, GameData data)
    {
        Name = name;
        Day = 1;
        Quality = graphicsManager.SelectedQuality;
        Resolution = graphicsManager.SelectedResolution;
        Happiness = data.Happiness;
        Points = data.Points;
        BackgroundVol = soundManager.Background;
        EffectsVol = soundManager.Effects;
    }
    public SaveData(string name, int day, string quality, Resolution resolution, float happiness, int points, float backgroundVol, float effectsVol, float voicesVol)
    {
        Name = name;
        Day = day;
        Quality = quality;
        Resolution = resolution;
        Happiness = happiness;
        Points = points;
        BackgroundVol = backgroundVol;
        EffectsVol = effectsVol;
        VoicesVol = voicesVol;
    }
}
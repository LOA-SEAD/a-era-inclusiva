using System;
using System.Linq;
using UnityEngine;

public class ConfigData
{
    public bool Fullscreen;
    public int Height;
    public int Width;
    public int RefreshRate;
    public float BackgroundVol;
    public float EffectsVol;



    public ConfigData(GraphicsManager graphicsManager, SoundManager soundManager)
    {
        var res = graphicsManager.SelectedResolution;
        Height = res.height;
        Width = res.width;
        RefreshRate = res.refreshRate;
        BackgroundVol = soundManager.Background;
        EffectsVol = soundManager.Effects;
        Fullscreen = graphicsManager.IsFullscreen;
    }

    public ConfigData()
    {
        var res = Screen.resolutions.Last();
        Fullscreen = true;
        Height = res.height;
        Width = res.width;
        RefreshRate = res.refreshRate;
        BackgroundVol = 100;
        EffectsVol = 100;
    }
    

}
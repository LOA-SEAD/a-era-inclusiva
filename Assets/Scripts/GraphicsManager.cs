using System;
using System.Linq;
using UnityEngine;

public class GraphicsManager
{
    private Resolution _selectedResolution;
    private bool _isFullscreen;
    private string _selectedQuality;
    
    public string SelectedQuality
    {
        get { return _selectedQuality; }
        set
        {
            var qualities = QualitySettings.names;
            if (!qualities.Contains(value))
            {
                _selectedQuality = qualities.Last();
                return;
            }
            _selectedQuality = value;
            
            QualitySettings.SetQualityLevel(Array.IndexOf(qualities,_selectedQuality), true);        }
    }

    public Resolution SelectedResolution
    {
        get { return _selectedResolution; }
        set
        {
            _selectedResolution = value;
            Screen.SetResolution(_selectedResolution.width, _selectedResolution.width, Screen.fullScreen);
        }
    }

    public bool IsFullscreen
    {
        get => _isFullscreen;
        set
        {
            _isFullscreen = value;
            Screen.fullScreen = _isFullscreen;
        }
    }

    public GraphicsManager(ConfigData data)
    {
        Resolution resolution = new Resolution
        {
            height = data.Height,
            width = data.Width,
            refreshRate = data.RefreshRate
        };
        SelectedResolution = resolution;
        IsFullscreen = data.Fullscreen;
    }

    public void Load(object obj, EventArgs e)
    {
        var data = (ConfigData) obj;
        Resolution resolution = new Resolution
        {
            height = data.Height,
            width = data.Width,
            refreshRate = data.RefreshRate
        };
        SelectedResolution = resolution;
        IsFullscreen = data.Fullscreen;
        
    }


    public GraphicsManager()
    {
        SelectedQuality = QualitySettings.names.Last();
        SelectedResolution = Screen.resolutions.Last();
        IsFullscreen = true;
    }
}
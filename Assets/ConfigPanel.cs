using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public TMP_Dropdown screenResDropdown;
    
    public Slider audioEffectsSlider;
    public Slider audioBackgroundSlider;

    public Toggle fullscreenToggle;

    private ConfigData _configData;
    // Start is called before the first frame update
    void Start()
    {
        PopulateScreensizeList();
    }

    void Awake()
    {
        _configData = GameManager.ConfigData;
    }

    void PopulateScreensizeList()
    {
        var screenOptions = Screen.resolutions.Select(res => new TMP_Dropdown.OptionData(res.ToString())).ToList();

        screenResDropdown.options = screenOptions;
    }
  
    

    public void OnResolutionChanged(int index)
    {
        var res = Screen.resolutions[index];
        _configData.Height = res.height;
        _configData.Width = res.width;
        _configData.RefreshRate = res.refreshRate;

    }
    public void OnBackgroundChanged(float value)
    {
        _configData.BackgroundVol = value;

    }
    public void OnEffectChanged(float value)
    {
        _configData.EffectsVol = value;


    }

    public void OnFullscreenChanged(bool value)
    {
        _configData.Fullscreen = value;

    }

    public void Save()
    {
        GameManager.ConfigData = _configData;
        GameManager.SaveManager.SaveConfig(GameManager.ConfigData);
        Debug.Log(GameManager.ConfigData.Height);
    }
}
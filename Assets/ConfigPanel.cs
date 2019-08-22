using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public TMP_Dropdown screenResDropdown;

    public TMP_Dropdown graphicsDropdown;

    public Slider audioEffectsSlider;
    public Slider audioBackgroundSlider;

    public Toggle fullscreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        PopulateScreensizeList();
        PopulateQualitiesList();
    }

    void PopulateScreensizeList()
    {
        var screenOptions = Screen.resolutions.Select(res => new TMP_Dropdown.OptionData(res.ToString())).ToList();

        screenResDropdown.options = screenOptions;
    }
    void PopulateQualitiesList()
    {
        var graphicOptions = QualitySettings.names.Select(res => new TMP_Dropdown.OptionData(res.ToString())).ToList();

        graphicsDropdown.options = graphicOptions;
    }
    
    public void OnQualityChanged(int index)
    {
        GameManager.GraphicsManager.SelectedQuality = graphicsDropdown.options[index].text;
        Debug.Log(index);
    }
    public void OnResolutionChanged(int index)
    {
        
        GameManager.GraphicsManager.SelectedResolution = Screen.resolutions[index];

        Debug.Log(index);

    }
    public void OnBackgroundChanged(float value)
    {
        GameManager.SoundManager.Background = value;
        Debug.Log(value);

    }
    public void OnEffectChanged(float value)
    {
        GameManager.SoundManager.Effects = value;

        Debug.Log(value);

    }

    public void OnFullscreenChanged(bool value)
    {
        GameManager.GraphicsManager.IsFullscreen  =value;
        Debug.Log(value);

    }
}
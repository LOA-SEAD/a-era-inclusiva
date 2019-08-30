using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public Slider audioBackgroundSlider;

    public Slider audioEffectsSlider;

    public Toggle fullscreenToggle;

    public TMP_Dropdown screenResDropdown;

// Start is called before the first frame update
    private void Start()
    {

#if !UNITY_STANDALONE
        fullscreenToggle.gameObject.SetActive(false);
        screenResDropdown.gameObject.SetActive(false);
#else
        PopulateScreensizeList();
#endif
    }
    


    private void PopulateScreensizeList()
    {
        var screenOptions = Screen.resolutions.Select(res => new TMP_Dropdown.OptionData(res.ToString())).ToList();

        screenResDropdown.options = screenOptions;
    }


    public void OnResolutionChanged(int index)
    {
        var res = Screen.resolutions[index];
    }

    public void OnBackgroundChanged(float value)
    {
        GameManager.SoundManager.Background = value;
    }

    public void OnEffectChanged(float value)
    {
        GameManager.SoundManager.Effects = value;
    }

    public void OnFullscreenChanged(bool value)
    {
        Screen.fullScreen = value;
    }
}
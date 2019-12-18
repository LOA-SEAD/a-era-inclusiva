using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI fullscreenToggle;
    public Animator animator;
    public bool Shown = false;
    public bool sliderShown;
    private void Start()
    {
     #if !UNITY_STANDALONE
             fullscreenToggle.transform.parent.gameObject.SetActive(false);
     #else
             fullscreenToggle.text = Screen.fullScreen ? "Trocar para modo janela" : "Trocar para tela cheia";
     
     #endif
    }

    public void ShowMusicSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = AudioManager.instance.MusicVolume;
        slider.onValueChanged.AddListener(OnMusicChanged);

        animator.SetTrigger("SlideIn");
        sliderShown = true;
    }
    
    public void ShowEffectSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = AudioManager.instance.SFXVolume;
        slider.onValueChanged.AddListener(OnEffectChanged);

        animator.SetTrigger("SlideIn");
        sliderShown = true;
    }

    
    public void OnMusicChanged(float value)
    {
        AudioManager.instance.MusicVolume = value;
        AudioManager.instance.Save();
    }

    public void OnEffectChanged(float value)
    {
        AudioManager.instance.SFXVolume = value;
        AudioManager.instance.AmbienceVolume = value * 0.30f;
        AudioManager.instance.PlaySfx((int) SoundType.Beep);
        AudioManager.instance.Save();
    }

    public void OnFullscreenChanged()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
            fullscreenToggle.text = "Trocar para tela cheia";

        }
        else
        {
            Resolution res = Screen.resolutions.Last();
            Screen.SetResolution(res.width, res.width, FullScreenMode.MaximizedWindow);
            fullscreenToggle.text = "Trocar para modo janela";
        }
    }
    public void OnAccessibilityChanged()
    {
        GameManager.AccessibilityMode = !GameManager.AccessibilityMode;
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        Shown = true;
       // GetComponentsInChildren<Button>().First().Select();
   
    }

    public void Hide()
    {
        if (!Shown) return;
        animator.SetTrigger("Hide");
        Shown = false;
        gameObject.SetActive(false);
      

    }

    public void HideSlider()
    {
        animator.SetTrigger("SlideOut");
        sliderShown = false;
    }

    public void IncSlider()
    {
        slider.value += 0.1f;
    }

    public void DecSlider()
    {
        slider.value -= 0.1f;
    }
}
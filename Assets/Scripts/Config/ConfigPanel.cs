using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI fullscreenToggle;
    public TextMeshProUGUI accessibilityMode;
    public Animator animator;
    public bool Shown;
    public bool sliderShown;
    private void Start()
    {
     #if !UNITY_STANDALONE
             fullscreenToggle.transform.parent.gameObject.SetActive(false);
     #else
             fullscreenToggle.text = Screen.fullScreen ? "Trocar para modo janela" : "Trocar para tela cheia";
             accessibilityMode.text = GameManager.AccessibilityMode ? "Desativar acessibilidade" : "Ativar acessibilidade";
     
     #endif
    }

    public void ShowBackgroundSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = AudioManager.instance.ambience.volume;
        slider.onValueChanged.AddListener(OnBackgroundChanged);

        animator.SetTrigger("SlideIn");
        sliderShown = true;
    }
    
    public void ShowEffectSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = AudioManager.instance.effects.volume;
        slider.onValueChanged.AddListener(OnEffectChanged);

        animator.SetTrigger("SlideIn");
        sliderShown = true;
    }

    
    public void OnBackgroundChanged(float value)
    {
        AudioManager.instance.ambience.volume = value;
        AudioManager.instance.Save();
    }

    public void OnEffectChanged(float value)
    {
        AudioManager.instance.effects.volume = value;
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
        accessibilityMode.text = GameManager.AccessibilityMode ? "Desativar acessibilidade" : "Ativar acessibilidade";
    }

    public void Show()
    {
        AudioManager.FadeInAmbience((int)SoundType.AmbienceHallway, 0.05f);
        animator.SetTrigger("Show");
        Shown = true;
    }

    public void Hide()
    {
        AudioManager.FadeOutAmbience(0.01f);
        animator.SetTrigger("Hide");
        Shown = false;

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
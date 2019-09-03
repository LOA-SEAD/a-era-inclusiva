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
    public AudioSource sliderAudioSource;
    public bool Shown;
    private void Start()
    {
#if !UNITY_STANDALONE
        fullscreenToggle.gameObject.SetActive(false);
#else
        fullscreenToggle.text = Screen.fullScreen ? "Trocar para modo janela" : "Trocar para tela cheia";
        accessibilityMode.text = GameManager.AccessibilityMode ? "Desativar acessibilidade" : "Ativar acessibilidade";

#endif
    }

    public void ShowBackgroundSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = GameManager.SoundManager.Background;
        slider.onValueChanged.AddListener(OnBackgroundChanged);

        animator.SetTrigger("SlideIn");

        slider.Select();
    }
    
    public void ShowEffectSlider()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.value = GameManager.SoundManager.Effects;
        slider.onValueChanged.AddListener(OnEffectChanged);

        animator.SetTrigger("SlideIn");

        slider.Select();
    }

    
    public void OnBackgroundChanged(float value)
    {
        sliderAudioSource.volume = value;
        GameManager.SoundManager.Background = value;
    }

    public void OnEffectChanged(float value)
    {
        sliderAudioSource.volume = value;
        GameManager.SoundManager.Effects = value;
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

        animator.SetTrigger("Show");
        Shown = true;
    }

    public void Hide()
    {
     
        animator.SetTrigger("Hide");
        Shown = false;

    }
}
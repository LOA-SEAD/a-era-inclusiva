using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool _shown;
    public Animator animator;
    public Image bg;
    public ConfigPanel configPanel;
    public Input input;
    private void Start()
    {
        bg.raycastTarget = false;
        if (FindObjectsOfType<Menu>().Length > 1)
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
       

    }

    public void Toggle()
    {
        if (_shown)
            Hide();
        else
            Show();
        
    }

    public void Show()
    {
        if (configPanel.Shown)
            return;
        bg.raycastTarget = true;
        animator.SetTrigger("Show");
        _shown = true;

    }

    public void Hide()
    {
     
        bg.raycastTarget = false;
        animator.SetTrigger("Hide");
        _shown = false;

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowConfig()
    {
        if(_shown)
            Hide();
        //animator.SetTrigger("Hide");

        configPanel.Show();
    }
    public void HideConfig()
    {
        configPanel.Hide();
    }

    public void ClickOutside()
    {
        if (configPanel.Shown)
        {
            if(configPanel.sliderShown)
                configPanel.HideSlider();
            else
                configPanel.Hide();
        }
        else if(_shown)
            Hide(); }
}
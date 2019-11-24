using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private UIMaster uiMaster;
    private Dictionary<Selectable, bool> elementsToSet;
    private bool _shown;
    public Animator animator;
    public Image bg;
    public ConfigPanel configPanel;
    public Input input;
    private Button selectedBeforeMenu;

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
    private void OnEnable()
    {
        uiMaster.Enable();
        
    }

    private void OnDisable()
    {
        uiMaster.Disable();
    }
    private void Awake()
    {
        uiMaster = new UIMaster();
        uiMaster.UI.Settings.performed += ctx => Toggle() ;

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
        if(EventSystem.current.currentSelectedGameObject!=null)
            selectedBeforeMenu = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        configPanel.gameObject.SetActive(true);
        GetComponentInChildren<Button>().Select();
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
                    selectedBeforeMenu?.Select();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleConfig()
    {

        if (configPanel.Shown)
        {
            configPanel.gameObject.SetActive(false);
            configPanel.Hide();
        }
        else
        {
            if(_shown)
                Hide();
            configPanel.gameObject.SetActive(true);
            configPanel.Show();
            configPanel.GetComponentInChildren<Button>().Select();
            
        }
    }

    public void ClickOutside()
    {
        if (configPanel.Shown)
        {
            if (configPanel.sliderShown)
                configPanel.HideSlider();
            else
            {
                animator.SetTrigger("Show");
                GetComponentInChildren<Button>().Select();
                _shown = true;
                configPanel.Hide();
            }
        }
        else if (_shown)
        {
            Hide();
        }
    }
}
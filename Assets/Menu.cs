
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool _shown;
    public Animator animator;
    public Image bg;


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
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        if(_shown)
            Hide();
        else
            Show();
    }
    public void Show()
    {
        bg.raycastTarget = true;
        _shown = true;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        bg.raycastTarget = false;

        _shown = false;
        animator.SetTrigger("Hide");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowConfig()
    {
    }

    public void Resume()
    {
        Hide();
    }


    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaTitulo : MonoBehaviour
{
    public GameObject invisibleButton;

    private void Start()
    {
        animator = instructions.GetComponent<Animator>();
    }

    public GameObject instructions;
    public GameObject buttons;
    private static readonly int Hiding = Animator.StringToHash("Hiding");
    private Animator animator;
    private bool menuVisible = false;


    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/ConversaAndre");
    }

    private void Update()
    {
        if (Input.anyKeyDown && !menuVisible)
        {
            HideInstructionsAndShowMenu();
        }
    }

    public void HideInstructionsAndShowMenu()
    {
        Destroy(invisibleButton);
        animator.SetBool(Hiding, true);
        Destroy(instructions, 1f);
        buttons.SetActive(true);
        menuVisible = true;
    }

    public void LeaveGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void ShowSettings()
    {
        //TODO
    }

}
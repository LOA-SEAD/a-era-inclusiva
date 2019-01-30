using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaTitulo : MonoBehaviour
{
    public GameObject botaoInvisivel;

    private void Start()
    {
        animator = instrucoes.GetComponent<Animator>();
    }

    public GameObject instrucoes;
    public GameObject botoes;
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
        Destroy(botaoInvisivel);
        animator.SetBool(Hiding, true);
        Destroy(instrucoes, 1f);
        botoes.SetActive(true);
        menuVisible = true;
    }

 
}
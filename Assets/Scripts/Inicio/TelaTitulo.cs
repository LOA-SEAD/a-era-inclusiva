using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaTitulo : MonoBehaviour

{
    public SceneController sceneController;
    public InputConfirmation inputConfirmation;
    public GameManager gameManager;
    public void Start()
    {
        inputConfirmation.acao += CreateGame;
    }

    private void CreateGame(string saveName)
    {
        gameManager.New(saveName);
        GetComponent<Animator>().SetTrigger("iniciar_jogo");

    }

    public void StartGame()
    {
        sceneController.ChangeTo("Scenes/ConversaAndre");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
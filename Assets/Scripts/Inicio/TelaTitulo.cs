using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaTitulo : MonoBehaviour

{
    public SceneController sceneController;
    public InputConfirmation inputConfirmation;
    public GameManager gameManager;
    public GameSetup gameSetup;
    public void Start()
    {
        inputConfirmation.acao += CreateGame;
    }

    private void CreateGame(string saveName)
    {
        gameSetup.Setup();
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
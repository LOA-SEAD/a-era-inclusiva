using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaTitulo : MonoBehaviour

{
    public SceneController sceneController;

    public void StartGame()
    {
        sceneController.ChangeTo("Scenes/ConversaAndre");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
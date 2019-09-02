using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelaTitulo : MonoBehaviour

{
    public GameManager gameManager;
    public InputConfirmation inputConfirmation;
    public SceneController sceneController;
    public Button StartButton;
    public void Start()
    {
        if (GameManager.SaveManager.SaveExists("save"))
        {
            StartButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf04b  Continuar";
        }
        else
        {
            StartButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf04b  Jogar";

        }

    }

    public void StartGame()
    {
        if (GameManager.SaveManager.SaveExists("save"))
        {
            GameManager.Load("save");
            sceneController.ChangeTo("Scenes/Corredor");

        }
        else
        {
            GameManager.New("save");
            sceneController.ChangeTo("Scenes/ConversaAndre");

        }

    }

    public void Exit()
    {
        Application.Quit();
    }
}
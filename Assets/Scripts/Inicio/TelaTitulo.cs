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
            StartButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf04b  Continuar";
        else
            StartButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf04b  Jogar";

        AudioManager.instance.PlayMusic((int)SoundType.MusicBeginning);
    }

    public void StartGame()
    {
        if (GameManager.PlayerData!=null)
        {
            sceneController.ChangeTo("Scenes/Corredor");

        }
        else
        {
            GameManager.New();
            sceneController.ChangeTo("Scenes/ConversaAndre");

        }

    }

    public void Exit()
    {
        Application.Quit();
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelaTitulo : MonoBehaviour

{
    public GameManager gameManager;
    public InputConfirmation inputConfirmation;
    public SceneController sceneController;
    public Button ContinueButton;
    public Confirmation newGameConfirmation;
    public void Start()
    {
        if (!GameManager.SaveManager.SaveExists("save"))
            ContinueButton.gameObject.SetActive(false);


        AudioManager.instance.PlayMusic((int)SoundType.MusicBeginning);
    }

    public void StartGame()
    {
    
            GameManager.New();
            sceneController.ChangeTo("Scenes/EscolhaAvatar");
            

    }

    public void OnNewGame()
    {
        if (GameManager.PlayerData != null)
        {
            newGameConfirmation.gameObject.SetActive(true);
        }
        else
        {
            StartGame();
        }
    }

    public void LoadGame()
    {
        if (GameManager.PlayerData!=null)
        {
            sceneController.ChangeTo("Scenes/Corredor");

        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
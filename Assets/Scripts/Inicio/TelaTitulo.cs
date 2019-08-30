using UnityEngine;

public class TelaTitulo : MonoBehaviour

{
    public GameManager gameManager;
    public GameSetup gameSetup;
    public InputConfirmation inputConfirmation;
    public SceneController sceneController;

    public void Start()
    {
        inputConfirmation.OnAccept(delegate { CreateGame(inputConfirmation.InputField.text); });
    }

    private void CreateGame(string saveName)
    {
        GameManager.New(saveName);
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
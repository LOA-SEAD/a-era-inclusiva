using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool StartAtStartScreen = true;
    public string StartScreen = "Scenes/TelaTitulo";
    public GameManager gameManager;
    
    private void Start()
    {

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (!StartAtStartScreen || SceneManager.GetActiveScene().name == StartScreen) return;
        if (GameManager.GameData == null)
            SceneManager.LoadScene(0);

    }

    public void ChangeTo(string scene)
    {
        //StartCoroutine(LowerSound());
        Initiate.Fade(scene,Color.black, 3);
    }
    

    public void LeaveGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

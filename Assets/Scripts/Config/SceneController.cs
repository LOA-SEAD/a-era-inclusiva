using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameManager gameManager;
    public bool StartAtStartScreen = true;
    public string StartScreen = "Scenes/TelaTitulo";

    private void Start()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (!StartAtStartScreen || SceneManager.GetActiveScene().name == StartScreen) return;
        if (GameManager.IsLoaded == false)
            SceneManager.LoadScene(0);
    }

    public void ChangeTo(string scene)
    {
        //StartCoroutine(LowerSound());
        Initiate.Fade(scene, Color.black, 3);
    }


    public void LeaveGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
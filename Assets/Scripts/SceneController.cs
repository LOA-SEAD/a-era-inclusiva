using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool StartAtStartScreen = true;
    private void Start()
    {
        if (!StartAtStartScreen) return;
        if (Game.Actions == null || Game.Demands == null || Game.Students == null || Game.Characters == null)
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

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{


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
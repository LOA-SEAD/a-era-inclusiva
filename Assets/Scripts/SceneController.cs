using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool StartAtStartScreen = true;
    public SoundController soundController;
    private void Start()
    {
        if (!StartAtStartScreen) return;
        if (Game.Actions == null || Game.Demands == null || Game.Students == null || Game.Characters == null)
            SceneManager.LoadScene(0);
        if (soundController == null)
        {
            soundController = FindObjectOfType<SoundController>();
        }
    }

    public void ChangeTo(string scene)
    {
        StartCoroutine(LowerSound());
        Initiate.Fade(scene,Color.black, 3);
    }

    private IEnumerator LowerSound()
    {
        float vol = soundController.Volume;
        while (!Mathf.Approximately(vol, 0))
        {
            vol = Mathf.Lerp(vol , 0,Time.deltaTime*5);
            soundController.Volume = vol;
        }
        yield return null;

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

using System.Linq;
using UnityEngine;

public class ControladorCorredor : MonoBehaviour
{
    public GameObject dialog;
    public SceneController sceneController;

    private void Start()
    {
        AudioManager.instance.PlayMusic((int) SoundType.MusicRoom);
        AudioManager.instance.PlayAmbience((int) SoundType.AmbienceHallway);
    }

    public void TryToStartClass()
    {
        if (GameManager.PlayerData.SelectedActions.Count == 9)
            sceneController.ChangeTo("Scenes/SalaDeAula");
        else
            dialog.SetActive(true);
    }

}
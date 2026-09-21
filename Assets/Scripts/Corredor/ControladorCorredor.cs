using UnityEngine;

public class ControladorCorredor : MonoBehaviour
{
    public GameObject dialog;
    public SceneController sceneController;

    private void Start()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
        AudioManager.instance.PlayAmbience((int)SoundType.AmbienceHallway);

        Debug.Log("Corredor carregado. AulaConcluida = " + GameManager.PlayerData.AulaConcluida);
    }

    public void TryToStartClass()
    {
        Debug.Log("Tentando iniciar aula. AulaConcluida = " + GameManager.PlayerData.AulaConcluida);

        if (GameManager.PlayerData.AulaConcluida)
        {
            Debug.Log("Aula já concluída. Indo direto para HTPI.");
            sceneController.ChangeTo("Scenes/HTPI");
            return;
        }

        if (GameManager.PlayerData.HasNineMethodologies())
        {
            Debug.Log("Indo para Sala de Aula.");
            sceneController.ChangeTo("Scenes/SalaDeAula");
        }
        else
        {
            Debug.Log("Ainda não escolheu as 9 metodologias.");
            dialog.SetActive(true);
        }
    }
}
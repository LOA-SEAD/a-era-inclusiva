using System.Linq;
using UnityEngine;

public class ControladorCorredor : MonoBehaviour
{
    public GameObject dialog;
    public SceneController sceneController;

    public void TryToStartClass()
    {
        if (GameManager.GameData.acoes.Count(x => x.selected) == 9)
            sceneController.ChangeTo("Scenes/SalaDeAula");
        else
            dialog.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
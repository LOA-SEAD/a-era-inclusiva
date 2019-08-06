using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCorredor : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject dialog;
    public void TryToStartClass()
    {
        if (Game.Actions.acoes.Count(x => x.selected) == 9)
        {
            sceneController.ChangeTo("Scenes/SalaDeAula");
        }
        else
        {
            dialog.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

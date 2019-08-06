using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public ControladorSalaDeAula csd;

    public TextMeshProUGUI tempo;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        tempo.SetText("Tempo: " + csd.levelTimeInSeconds.ToString("0"));
    }
}

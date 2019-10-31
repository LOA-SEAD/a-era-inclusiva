using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSalaProfessores : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
    }
}

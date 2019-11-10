using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSalaProfessores : MonoBehaviour
{
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);

    }
    
    
}

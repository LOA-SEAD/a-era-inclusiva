using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisoFimAula : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

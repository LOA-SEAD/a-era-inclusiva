using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tablet : MonoBehaviour
{
    public UnityEvent beforeFade;
    public UnityEvent afterFade;

    void BeforeFade()
    {
        beforeFade.Invoke();
    }

    void AfterFade()
    {
        AudioManager.instance.StopAmbience();
        AudioManager.instance.PlayMusic((int)SoundType.MusicTablet);

        afterFade.Invoke();
    }
}

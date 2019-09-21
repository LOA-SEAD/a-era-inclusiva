using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tablet : MonoBehaviour
{
    public UnityEvent beforeFade;
    public UnityEvent afterFade;

    // Start is called before the first frame update
    void BeforeFade()
    {
        beforeFade.Invoke();
    }

    // Update is called once per frame
    void AfterFade()
    {
        afterFade.Invoke();

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Button select;
    public bool Auto;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Auto)
            Select();

    }

    public void Select()
    {
        if (select != null)
            select.Select();
        else {
             GetComponentsInChildren<Button>().First(x => x.interactable).Select();
            
        }
    }

}
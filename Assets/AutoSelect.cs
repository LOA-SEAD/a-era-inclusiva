using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelect : MonoBehaviour
{
    public Selectable select;

    // Start is called before the first frame update
    void OnEnable()
    {
      Select();
        
    }

    public void Select()
    {
        if (select != null)
            select.Select();
        else
            GetComponentInChildren<Selectable>().Select();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
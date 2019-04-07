using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class AcoesHTPI : MonoBehaviour
{
    public List<string> Icons;
    public List<TextMeshProUGUI> IconObjects;

    // Update is called once per frame
    void Start()
    {
        SetIcons();
    }
    void SetIcons()
    {
        for(int i=0; i< IconObjects.Count; i++) {
            IconObjects[i].SetText(Icons[i]);
            IconObjects[i].GraphicUpdateComplete();

        }
    }
}

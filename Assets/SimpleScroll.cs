using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScroll : MonoBehaviour
{
    public float step = 80;
    public Button UpButton;
    public Button DownButton;
    public bool autoStepSize;
    public GameObject parent;
    public int childrenCount;
    private Vector3 localPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        childrenCount = parent.transform.childCount;
        localPosition = parent.transform.localPosition;
        if (autoStepSize)
        {
            step = parent.GetComponentInChildren<RectTransform>().rect.height;
        }
       UpButton.onClick.AddListener(delegate
       {
           if (!(localPosition.y - step > -2*step)) return;
           localPosition.y -= step;
           parent.transform.localPosition = localPosition;
       }); 
       DownButton.onClick.AddListener(delegate
       {
           if (localPosition.y + step > (childrenCount-2)*step) return;

           localPosition.y += step;
           parent.transform.localPosition = localPosition;
       }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSize : MonoBehaviour
{
    private int count;
    public float Spacing = 10;
    public int MaxShown = 3;
    public GameObject parent;
    private float height;
    // Start is called before the first frame update
    void OnEnable()
    {
        height = (GetComponent<RectTransform>().rect.height + Spacing) / MaxShown;
        Debug.Log(height);
        count = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count != parent.transform.childCount)
        {
            fixHeight();
            count = parent.transform.childCount;
        }
    }

    void fixHeight()
    {
        height = (GetComponent<RectTransform>().rect.height + Spacing) / MaxShown;

        foreach (Transform obj in parent.transform)
        {
            obj.gameObject.GetComponent<RectTransform>()
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,height - Spacing);
            obj.localScale = Vector3.one;
        }
    }
}

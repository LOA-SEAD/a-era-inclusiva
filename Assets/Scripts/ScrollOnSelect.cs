using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollOnSelect : MonoBehaviour
{
    public SimpleScroll scroll;
    private GameObject LastSelectedObject;
    private GameObject SelectedObject
    {
        get
        {
            return EventSystem.current.currentSelectedGameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedObject!=null && LastSelectedObject != SelectedObject)
        {
            if(SelectedObject.transform.parent == transform) {
                LastSelectedObject = SelectedObject;
                scroll.GoTo(LastSelectedObject.transform.GetSiblingIndex());
            }
        }
    }
}

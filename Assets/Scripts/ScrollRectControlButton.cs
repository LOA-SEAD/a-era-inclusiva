using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectControlButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ScrollRectScript scrollRectScript;
    [SerializeField] private bool isDownButton;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDownButton)
        {
            scrollRectScript.ButtonDownIsPressed();
        }
        else
        {
            scrollRectScript.ButtonUpIsPressed();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class ScrollRectScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform scrollRectTransform;
    private bool mouseDown, buttonDown, buttonUp;
    private float normVertPos;
    private List<Transform> demandToggles;
    private int selectedDemandIndex, distanceBetweenObjects;

    void Start()
    {   
        demandToggles = new List<Transform>();
        selectedDemandIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        demandToggles.Clear();
        if (scrollRectTransform.childCount != 0)
        {
            if (selectedDemandIndex < 0)
                selectedDemandIndex = scrollRectTransform.childCount - 1;
            else if (selectedDemandIndex >= scrollRectTransform.childCount)
                selectedDemandIndex = 0;
            
            for (int i = 0; i < scrollRectTransform.childCount; i++)
            {
                demandToggles.Add(scrollRectTransform.GetChild(i));
            }
        }
        
        if (mouseDown)
        {
            if (buttonDown)
                ScrollDown();
            else if (buttonUp)
                ScrollUp();
        }
    }

    public void ButtonDownIsPressed()
    {
        mouseDown = true;
        buttonDown = true;
    }

    public void ButtonUpIsPressed()
    {
        mouseDown = true;
        buttonUp = true;
    }

    private void ScrollDown()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonDown = false;
        }
        else
        {
            selectedDemandIndex += 1;
            normVertPos = scrollRect.verticalNormalizedPosition;
            normVertPos += 0.01f;
            scrollRect.verticalNormalizedPosition = normVertPos;
        }
    }

    private void ScrollUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonUp = false;
        }
        else
        {
            selectedDemandIndex-= 1;    
            normVertPos = scrollRect.verticalNormalizedPosition;
            normVertPos -= 0.01f;
            scrollRect.verticalNormalizedPosition = normVertPos;
        }
    }
    
    void LerpToBttn(int position)
    {
        float newY = Mathf.Lerp(position, scrollRectTransform.anchoredPosition.x, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(scrollRectTransform.anchoredPosition.x, newY );

        scrollRectTransform.anchoredPosition = newPosition;
    }
}

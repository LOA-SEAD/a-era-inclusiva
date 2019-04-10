using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollRectScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    private bool mouseDown, buttonDown, buttonUp;
    private float normVertPos;
    
    // Start is called before the first frame update
    void Start()
    {    
        Debug.Log("Nome do componente: "+ scrollRect.name);
        //scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDown)
        {
            Debug.Log("Entrou em mouseDown");
            if (buttonDown)
                ScrollDown();
            else if (buttonUp)
                ScrollUp();
        }
    }

    public void ButtonDownIsPressed()
    {
        Debug.Log("ButtonDownIsPressed");
        mouseDown = true;
        buttonDown = true;
    }

    public void ButtonUpIsPressed()
    {
        Debug.Log("ButtonUpIsPressed");
        mouseDown = true;
        buttonUp = true;
    }

    private void ScrollDown()
    {
        Debug.Log("Entrou em ScrollDown");
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonDown = false;
        }
        else
        {
            normVertPos = scrollRect.verticalNormalizedPosition;
            Debug.Log("1. Posição na vertical: "+normVertPos);
            normVertPos += 0.01f;
            scrollRect.verticalNormalizedPosition = normVertPos;
            Debug.Log("2. Posição na vertical: "+normVertPos);
        }
    }

    private void ScrollUp()
    {
        Debug.Log("entrou em ScrollUp");
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonUp = false;
        }
        else
        {
            normVertPos = scrollRect.verticalNormalizedPosition;
            Debug.Log("1. Posição na vertical: "+normVertPos);
            normVertPos -= 0.01f;
            scrollRect.verticalNormalizedPosition = normVertPos;
            Debug.Log("2. Posição na vertical: "+normVertPos);
        }
    }
}

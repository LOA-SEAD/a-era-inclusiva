using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    /*Script that must be added to the panel that "fathers" the scrollpanel so the generated buttons always snaps to
     *the "center" (or any point really) */
    
    public RectTransform scrollPanel;
    public Button[] bttns;
    public RectTransform center;//center to compare the distance for each button

    private float[] distance; //All buttons' distance to the center
    private bool dragging;
    private int bttnDistance; //distance btween the buttons
    private int minButtonNum; //To hold the number of the button with the smallest distance to center
    // Start is called before the first frame update
    void Start()
    {
        int bttnLength = bttns.Length;
        distance = new float[bttnLength];
        
        //Get distance between buttons
        bttnDistance = (int) Mathf.Abs(bttns[1].GetComponent<RectTransform>().anchoredPosition.x - bttns[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bttns.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - bttns[i].transform.position.x);
        }
        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < bttns.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        if (!dragging)
        {
            LerpToBttn(minButtonNum * -bttnDistance);
        }
    }

    void LerpToBttn(int position)
    {
        float newX = Mathf.Lerp(scrollPanel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);

        scrollPanel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
    
}

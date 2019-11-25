using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControladorVisualizadorRM : MonoBehaviour
{    private Button selectedBeforeMenu;
    public Button returnButton;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            selectedBeforeMenu = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        returnButton.Select();
    }

    public void Restore()
    {
        selectedBeforeMenu.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

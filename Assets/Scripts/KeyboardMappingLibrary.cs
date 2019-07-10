using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardMappingLibrary : MonoBehaviour
{
    public EventSystem m_EventSystem;
    public Transform optionListBoxTransform;
    public Transform originButton;

    private List<Transform> optionList;
    private int selectedIndex = 0, listLength;
    private Color buttonDefault;
    // Start is called before the first frame update
    void Start()
    {
        optionList = new List<Transform>();
        foreach (Transform child in optionListBoxTransform.GetChild(0))
        {
            Debug.Log("Child: "+ child.name);
            optionList.Add(child);
        }
        listLength = (optionList.Count - 1);
        buttonDefault = optionList[selectedIndex].GetChild(1).GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down"))
        {
            optionList[selectedIndex].GetChild(1).GetComponent<Image>().color = buttonDefault;
            if (selectedIndex == listLength)
                selectedIndex = 0;
            else
                selectedIndex++;
        }

        if (Input.GetKeyDown("up"))
        {
            optionList[selectedIndex].GetChild(1).GetComponent<Image>().color = buttonDefault;
            if (selectedIndex == 0)
                selectedIndex = listLength;
            else
                selectedIndex--;
        }

        if (Input.GetKeyDown("left"))
        {
            StartCoroutine("ReactivateButton");
            transform.parent.gameObject.SetActive(false);   
        }
        
        m_EventSystem.SetSelectedGameObject(optionList[selectedIndex].gameObject);
        optionList[selectedIndex].GetChild(1).GetComponent<Image>().color = Color.black;
    }

    IEnumerator ReactivateButton()
    {
        m_EventSystem.SetSelectedGameObject(null);
        yield return null;
        m_EventSystem.SetSelectedGameObject(m_EventSystem.firstSelectedGameObject);
    }
    
}

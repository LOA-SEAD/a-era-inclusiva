using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardMappingHallway : MonoBehaviour
{
    public Transform[] optionReferences;
    public Transform confirmation;
    private int option;

    private Transform confirmationMessage;
    
    // Start is called before the first frame update
    void Start()
    {  
        foreach (Transform child in confirmation.GetChild(0))
        {
            Debug.Log("Child: " +child.name+"\n");
            if (child.gameObject.name.Equals("Message"))
                confirmationMessage = child;
        }
        
    }

    // Update is called once per frame    
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            ShowMessage(0);
        }
        if (Input.GetKeyDown("right"))
        {
            ShowMessage(1);
        }
        if (Input.GetKeyDown("down"))
        {
            ShowMessage(2);  
        }

        if (Input.GetKeyDown("left"))
        {
           ShowMessage(3);
        }

        if (Input.GetKeyDown("enter"))
        {
            if(confirmation.gameObject.activeInHierarchy)
                GoToRoom();
        }
        if (Input.GetKeyDown("escape"))
            confirmation.gameObject.SetActive(false);
    }

    void ChangeMessage(string message, Transform gameObjectTransform)
    {
        gameObjectTransform.gameObject.GetComponent<TextMeshProUGUI>().text = message;
    }

    void ShowMessage(int optionReferenceIndex)
    {
        Debug.Log("Seta para cima clicada + mensagem: "+ confirmationMessage.gameObject.GetComponent<TextMeshProUGUI>().text);
        ChangeMessage("Deseja ir para " + optionReferences[optionReferenceIndex].gameObject.name+" ?", confirmationMessage);
        option = optionReferenceIndex;
        confirmation.gameObject.SetActive(true);
    }

    public void GoToRoom()
    {
        
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("Scenes/Biblioteca");
                break;
            case 1:
                SceneManager.LoadScene("Scenes/SalaDeAula");
                break;
            case 2:
                SceneManager.LoadScene("Scenes/SalaProfessores");
                break;
            case 3:
                SceneManager.LoadScene("Scenes/SalaRecursosMultifuncionais");
                break;
        }
    }
}

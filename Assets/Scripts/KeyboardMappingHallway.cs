using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class KeyboardMappingHallway : MonoBehaviour
{
    public Transform confirmation;
    public Transform confirmationMessage;
    public EventSystem eventSystem;
    private int listLength;

    private List<Transform> optionList;
    public Transform optionsParent;

    private int selectedOption;

    // Start is called before the first frame update
    private void Start()
    {
        optionList = new List<Transform>();
        foreach (Transform child in optionsParent.GetChild(0))
        {
            Debug.Log("Child: " + child.name + "\n");
            optionList.Add(child);
        }

        listLength = optionList.Count - 1;
    }

    // Update is called once per frame    
    private void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            if (selectedOption == 0)
                selectedOption = listLength;
            else
                selectedOption--;
            Debug.Log("Opção selecionada: " + selectedOption);

            eventSystem.SetSelectedGameObject(optionList[selectedOption].gameObject);
        }

        if (Input.GetKeyDown("down"))
        {
            if (selectedOption == listLength)
                selectedOption = 0;
            else
                selectedOption++;
            Debug.Log("Opção selecionada: " + selectedOption);
            eventSystem.SetSelectedGameObject(optionList[selectedOption].gameObject);
        }

        if (Input.GetKeyDown("enter") || Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            var message = "Você deseja ir para " + optionList[selectedOption].name;
            ChangeMessage(message, confirmationMessage);
            confirmation.gameObject.SetActive(true);
            if (confirmation.gameObject.activeInHierarchy)
                GoToRoom();
        }

        if (Input.GetKeyDown("escape"))
            if (confirmation.gameObject.activeInHierarchy)
                confirmation.gameObject.SetActive(false);
    }

    private void ChangeMessage(string message, Transform gameObjectTransform)
    {
        gameObjectTransform.gameObject.GetComponent<TextMeshProUGUI>().text = message;
    }

    public void GoToRoom()
    {
        switch (selectedOption)
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
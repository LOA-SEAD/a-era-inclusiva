using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StudentList : MonoBehaviour
{
    public ScrollList scrollList;
    public Button prefabButton;
    public TextMeshProUGUI studentName;
    public TextMeshProUGUI studentDescription;
    public Image studentPhoto;
    public bool importantOnly;
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";

    void Awake()
    {

        foreach (var student in Game.Students.alunos)
        {
            if (importantOnly && !student.importante)
            {
                continue;
            }

            var button = Instantiate(prefabButton);
            button.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(
                _characterPortraitLocation + student.id);
            button.onClick.AddListener(delegate { ShowStudent(student); });
            scrollList.AddGameObject(button.transform);
            button.transform.localScale = Vector3.one;
        }    }



    void ShowStudent(ClassAluno student)
    {
        if (student.id != 0)
        {
            studentPhoto.sprite = Resources.Load<Sprite>(
                _characterPortraitLocation + student.id);
        }

        studentName.SetText(student.nome);
        studentDescription.SetText(student.descricao);
    }
}
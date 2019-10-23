using System;
using UnityEngine;
using UnityEngine.UI;

public class StudentList:MonoBehaviour
{
    public SimpleScrollAlt ScrollAlt;
    public delegate void StudentAction(ClassAluno aluno);

    public bool importantOnly;
    public Selectable prefabButton;

    private StudentAction whenSelected;

    public void SetWhenSelectedAction(StudentAction action)
    {
        whenSelected = action;
    }


    public void Start()
    { 
        if (GameManager.GameData == null || !GameManager.GameData.Loaded) 
            GameData.GameDataLoaded += UpdateList;
        else {
            UpdateList(this, EventArgs.Empty);
        }
    }



    private void UpdateList(object sender, EventArgs e)
    {
        if (GameManager.GameData.Alunos == null ) return;
        ScrollAlt.Clear();
        ScrollAlt.BackToTop();
        foreach (var student in GameManager.GameData.Alunos)
        {
            if (importantOnly && !student.importante) continue;

            var studentIcon = Instantiate(prefabButton);
            studentIcon.GetComponent<StudentIcon>().Student = student;
            if (whenSelected != null)
                studentIcon.GetComponent<Button>().onClick.AddListener(delegate { whenSelected(student); });
            ScrollAlt.Add(studentIcon);
        }
        ScrollAlt.SelectFirst();
    }
}
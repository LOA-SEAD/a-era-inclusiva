using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StudentList : SimpleScroll
{
    public StudentIcon prefabButton;
   
    public bool importantOnly;


    new void Start()
    {
        base.Start();
        UpdateList();
    }

    private void UpdateList()
    {
        if (Game.Students.alunos == null) return;
        Clear();
        BackToTop();
        foreach (var student in Game.Students.alunos)
        {
            if (importantOnly && !student.importante)
            {
                continue;
            }

            var button = Instantiate(prefabButton);
            button.Student = student;
            button.AddListener(delegate { OnSelect(student); });
            Add(button.gameObject);
        }
    }

    protected virtual void OnSelect(ClassAluno student)
    {
        throw new System.NotImplementedException();
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StudentList : ScrollList<ClassAluno>
{
    public StudentIcon prefabButton;
   
    public bool importantOnly;


    new void Start()
    {
        base.Start();
        foreach (var student in Game.Students.alunos)
        {
            if (importantOnly && !student.importante)
            {
                continue;
            }

            var button = Instantiate(prefabButton);
            button.Student = student;
            button.AddListener(delegate { OnSelect(student); });
            AddGameObject(button.transform);
        }

        Selected = 0;


    }

  
}
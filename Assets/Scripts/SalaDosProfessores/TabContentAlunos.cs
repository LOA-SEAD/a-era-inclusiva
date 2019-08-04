using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabContentAlunos : MonoBehaviour
{
    public TextMeshProUGUI nome;
    public Image portrait;
    public TextMeshProUGUI description;
    public StudentList studentList;
    public void SetAluno(ClassAluno aluno)
    {
        nome.SetText(aluno.nome);
        portrait.sprite = aluno.LoadPortrait();
        description.SetText(aluno.descricao);

    }

    public void Awake()
    {
        SetAluno(Game.Students.alunos.First(x=>x.importante));
        studentList.SetWhenSelectedAction(SetAluno);

    }
}

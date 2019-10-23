using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabContentAlunos : MonoBehaviour
{
    public TextMeshProUGUI description;
    public TextMeshProUGUI nome;
    public StudentIcon portrait;
    public StudentList studentList;

    public void SetAluno(ClassAluno aluno)
    {
        nome.SetText(aluno.nome);
        portrait.Student = aluno;
        description.SetText(aluno.descricao);
    }

    public void Awake()
    {
        SetAluno(GameManager.GameData.Alunos.First(x => x.importante));
        studentList.SetWhenSelectedAction(SetAluno);
    }
}
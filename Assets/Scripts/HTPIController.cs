using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    private ClassAluno _selectedStudent;
    private Dictionary<ClassAluno, List<ClassAcao>> _selectedActions;
    public TextMeshProUGUI nameObj;
    public TextMeshProUGUI descriptionObj;
    public Image portrait;


    private void Start()
    {
        _selectedActions = new Dictionary<ClassAluno, List<ClassAcao>>();
        
    }

    public void AddAction(ClassAcao acao)
    {
        if (_selectedStudent == null)
        {
            return;
        }
        if (_selectedActions[_selectedStudent] == null)
        {
            _selectedActions[_selectedStudent] = new List<ClassAcao>();
        }

        if (_selectedActions[_selectedStudent].Count > 3)
        {
            Debug.Log("Cannot select more than three actions");
            return;
        }

        _selectedActions[_selectedStudent].Add(acao);
    }

    public void SelectStudent(ClassAluno student)
    {
        nameObj.SetText(student.nome);
        descriptionObj.SetText(student.descricao);
        portrait.sprite = student.LoadPortrait();
        _selectedStudent = student;
    }
}
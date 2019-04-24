using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    private ClassAluno _selectedStudent;

    public ClassAluno SelectedStudent
    {
        get { return _selectedStudent; }
        set
        {
            _selectedStudent = value;
            nameObj.SetText(_selectedStudent.nome);
            descriptionObj.SetText(_selectedStudent.descricao);
            portrait.sprite = _selectedStudent.LoadPortrait();
            selectedActionListHtpi.UpdateList();
        }
    }

    private Dictionary<ClassAluno, List<ClassAcao>> _selectedActions;
    public TextMeshProUGUI nameObj;
    public TextMeshProUGUI descriptionObj;
    public Image portrait;
    public SelectedActionListHTPI selectedActionListHtpi;


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

        if (!_selectedActions.ContainsKey(_selectedStudent) || _selectedActions[_selectedStudent] == null)
        {
            _selectedActions[_selectedStudent] = new List<ClassAcao>();
        }


        if (_selectedActions[_selectedStudent].Contains(acao))
        {
            _selectedActions[_selectedStudent].Remove(acao);
            selectedActionListHtpi.UpdateList();
            return;
        }

        if (_selectedActions[_selectedStudent].Count >= 3)
        {
            Debug.Log("Cannot select more than three actions");
            return;
        }

        _selectedActions[_selectedStudent].Add(acao);

        selectedActionListHtpi.UpdateList();
    }


    public List<ClassAcao> GetSelectedActions()
    {
        if (!_selectedActions.ContainsKey(_selectedStudent) || _selectedActions[_selectedStudent] == null)
        {
            _selectedActions[_selectedStudent] = new List<ClassAcao>();
        }

        return _selectedActions[_selectedStudent];
    }
}
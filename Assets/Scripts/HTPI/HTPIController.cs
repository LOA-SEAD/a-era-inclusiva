﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    public ActionConfirmation _actionConfirmation;
    public Confirmation _confirmation;
    private Dictionary<ClassAluno, List<ClassAcao>> _selectedActions;
    private ClassAluno _selectedStudent;
    public AcaoIcon acaoPrefab;
    public ActionList actionList;
    public TextMeshProUGUI descriptionObj;
    public GameObject EditandoAcoes;
    public Button EditButton;
    public TextMeshProUGUI nameObj;
    public Image portrait;
    public GameObject selectedActionListHtpi;
    public StudentList studentList;

    public ClassAluno SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            _selectedStudent = value;
            nameObj.SetText(_selectedStudent.nome);
            descriptionObj.SetText(_selectedStudent.descricao);
            portrait.sprite = _selectedStudent.LoadPortrait();
            ShowSelected();
        }
    }

    private void ShowSelected()
    {
        EditButton.interactable = true;

        foreach (Transform children in selectedActionListHtpi.transform) Destroy(children.gameObject);

        if (!_selectedActions.ContainsKey(_selectedStudent)) return;
        foreach (var action in _selectedActions[_selectedStudent])
        {
            var botao = Instantiate(acaoPrefab, selectedActionListHtpi.transform, false);
            botao.Acao = action;
        }
    }


    private void Start()
    {
        EditButton.interactable = false;
        _selectedActions = new Dictionary<ClassAluno, List<ClassAcao>>();
        _confirmation.OnAccept(delegate
        {
            EditandoAcoes.SetActive(true);
            _confirmation.Hide();
        });
        _confirmation.OnDeny(delegate { Initiate.Fade("Scene/Corredor", Color.black, 1); });
        actionList.SetWhenSelected(AddAction);
        studentList.SetWhenSelectedAction(student => { SelectedStudent = student; });
    }


    public void AddAction(ClassAcao acao)
    {
        if (_selectedStudent == null) return;

        if (!_selectedActions.ContainsKey(_selectedStudent) || _selectedActions[_selectedStudent] == null)
            _selectedActions[_selectedStudent] = new List<ClassAcao>();


        if (_selectedActions[_selectedStudent].Contains(acao))
        {
            _selectedActions[_selectedStudent].Remove(acao);
            ShowSelected();
            return;
        }

        if (_selectedActions[_selectedStudent].Count >= 3) return;
        _selectedActions[_selectedStudent].Add(acao);
        ShowSelected();

        if (_selectedActions.Sum(x => x.Value.Count) == 9)
        {
            ShowEndConfirmation();
            return;
        }

        if (_selectedActions[_selectedStudent].Count == 3) ShowConfirmation();
    }

    private void ShowConfirmation()
    {
        _actionConfirmation.gameObject.SetActive(true);
        _actionConfirmation.ActionsToShow = _selectedActions[_selectedStudent];
        _actionConfirmation.OnAccept(delegate { _actionConfirmation.gameObject.SetActive(false); });
        _actionConfirmation.OnDeny(delegate
        {
            _selectedActions[_selectedStudent].Clear();
            ShowSelected();

            _actionConfirmation.gameObject.SetActive(false);
        });
    }

    private void ShowEndConfirmation()
    {
        EditandoAcoes.SetActive(false);
        _confirmation.gameObject.SetActive(true);
        _confirmation.OnAccept(delegate
        {
            GameManager.PlayerData.Day++;
            GameManager.GameData.Acoes.ForEach(x => x.selected = false);
            SceneManager.LoadScene("Scenes/Corredor");
        });
        _confirmation.OnDeny(delegate
            {
                EditandoAcoes.SetActive(true);
                _confirmation.gameObject.SetActive(false);
            }
        );

        var points = CalculatePoints();

        _confirmation.SetText(
            "Você selecionou ações para todos os estudantes e obteve " + points +
            " pontos, deseja finalizar o HTPI e voltar para o corredor?");
    }

    private int CalculatePoints()
    {
        var totalPoints = 0;
        foreach (var student in _selectedActions)
        {
            var selectedActions = student.Value;
            var acoesEficazes = GameManager.GameData.Demandas.Find(x => x.student == student.Key).acoesEficazes;
            var points = acoesEficazes.Where(x => selectedActions.Exists(y => y.id == x.idAcao))
                .Sum(x => x.efetividade);
            totalPoints += points;
        }

        return totalPoints;
    }

    public List<ClassAcao> GetSelectedActions()
    {
        if (!_selectedActions.ContainsKey(_selectedStudent) || _selectedActions[_selectedStudent] == null)
            _selectedActions[_selectedStudent] = new List<ClassAcao>();

        return _selectedActions[_selectedStudent];
    }
}
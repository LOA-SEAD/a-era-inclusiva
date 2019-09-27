using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MetodologiasTab : MonoBehaviour
{
    private List<string> _types;
    private int _typeSelectedId;
    private bool _loaded;
    public Button actionButtonPrefab;
    public AcaoIcon acaoIconPrefab;
    public SimpleScroll actionList;
    public GameObject gridMetodologias;
    public ActionConfirmation Confirmation;
    public Confirmation EndConfirmation;
    private void Awake()
    {
        _typeSelectedId = -1;
        _types = new List<string>();
    }
    

    public void SetTypes()
    {
        foreach (var acao in GameManager.GameData.Acoes)
        {
            if (!_types.Contains(acao.tipo))
                _types.Add(acao.tipo);
        }
        _loaded = true;

        GoToNextMethodology();
    }


    private void Update()
    {
        if (!_loaded && GameManager.GameData.Loaded)
            SetTypes();
    }

    public void GoToNextMethodology()
    {

        Confirmation.gameObject.SetActive(false);
        _typeSelectedId++;
        if (_typeSelectedId > _types.Count)
        {
            ShowConfirmation();
            return;
        }

        foreach (Transform children in actionList.parent.transform)
        {
            Destroy(children.gameObject);
        }
        actionList.UpdateChildrenCount();
        var actions = GameManager.GameData.Acoes.Where(x => x.tipo == _types[_typeSelectedId]).ToList();
        foreach (var action in actions)
        {

            var button = Instantiate(actionButtonPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(action.icone + " " + action.nome);
            button.GetComponentInChildren<TextMeshProUGUI>().fontSize = 19;
            button.onClick.AddListener(() => Select(action));
            actionList.Add(button.gameObject);
        }

        UpdateGrid();
    }

    private void UpdateGrid()
    {
        foreach (Transform child in gridMetodologias.transform)
        {
            Destroy(child.gameObject);
        }

        var actions = GameManager.PlayerData.SelectedActions.Where(x => x.tipo == _types[_typeSelectedId]);
        foreach (var action in actions)
        {
            var button = Instantiate(acaoIconPrefab, gridMetodologias.transform);
            button.Acao = action;
        }
    }

    private void Select(ClassAcao action)
    {
        if (!GameManager.PlayerData.SelectedActions.Contains(action))
            GameManager.PlayerData.SelectedActions.Add(action);
        else
        {
            GameManager.PlayerData.SelectedActions.Remove(action);
        }


        UpdateGrid();
        if (GameManager.PlayerData.SelectedActions.Count(x => x.tipo == _types[_typeSelectedId]) == 3)
        {
            if (_typeSelectedId < _types.Count - 1)
                ShowConfirmation();
            else
                ShowEndingConfirmation();
        }




}


    private void ShowConfirmation()
    {
        if (_types.Count == _typeSelectedId)
        {
        }
        else
        {
            Confirmation.gameObject.SetActive(true);
            Confirmation.ActionsToShow = GameManager.PlayerData.SelectedActions
                .Where(x => x.tipo == _types[_typeSelectedId]).ToList();
            Confirmation.OnAccept(GoToNextMethodology);
            Confirmation.OnDeny(() => Confirmation.gameObject.SetActive(false));
        }
    }

    private void ShowEndingConfirmation()
    {
      EndConfirmation.gameObject.SetActive(true);  
    }

    public void Undo()
    {
        if (GameManager.PlayerData.SelectedActions.Count(x => x.tipo == _types[_typeSelectedId]) != 0)
        {
            GameManager.PlayerData.SelectedActions.RemoveWhere(x => x.tipo == _types[_typeSelectedId]);
            UpdateGrid();
        }
        else if (_typeSelectedId >= 1)
        {
            _typeSelectedId -= 2;
            GoToNextMethodology();
        }
          
        
    }
    private void Start()
    {
    }
}
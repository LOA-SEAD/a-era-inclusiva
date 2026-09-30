using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetodologiasTab : MonoBehaviour
{
    private string descriptionTemplate =
        @"Escolha 3 tipos de {0} que você considera mais eficazes para esta aula, levando em consideração os perfis dos estudantes";

    private List<string> _types;
    private int _typeSelectedId;
    private bool _loaded;

    public AcaoIcon acaoIconPrefabDialogo;
    public AcaoIcon acaoIconPrefabSala;
    public AcaoIcon acaoIconPrefabRecursos;
    public AcaoIcon acaoIconPrefab;

    public SimpleScroll actionList;
    public GameObject gridMetodologias;

    public ActionConfirmation Confirmation;
    public Confirmation EndConfirmation;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        _typeSelectedId = -1;
        _types = new List<string>();
    }

    private void Update()
    {
        if (!_loaded && GameManager.GameData.Loaded)
        {
            SetTypes();
        }
    }

    public void SetTypes()
    {
        foreach (var acao in GameManager.GameData.Acoes)
        {
            if (!_types.Contains(acao.tipo))
            {
                _types.Add(acao.tipo);
            }
        }

        _loaded = true;
        GoToNextMethodology();
    }

    public void GoToNextMethodology()
    {
        Confirmation.gameObject.SetActive(false);

        _typeSelectedId++;

        if (_typeSelectedId >= _types.Count)
        {
            ShowEndingConfirmation();
            return;
        }

        titleText.SetText(_types[_typeSelectedId]);
        descriptionText.SetText(string.Format(descriptionTemplate, _types[_typeSelectedId]));

        actionList.Clear();

        var actions = GameManager.GameData.Acoes
            .Where(x => x.tipo == _types[_typeSelectedId] && x.diaMin <= GameManager.PlayerData.Day)
            .ToList();

        foreach (var action in actions)
        {
            var button = Instantiate(
                action.tipo == "Diálogos"
                    ? acaoIconPrefabDialogo
                    : action.tipo == "Recursos"
                        ? acaoIconPrefabRecursos
                        : acaoIconPrefabSala
            );

            button.Acao = action;
            button.GetComponent<Button>().onClick.AddListener(() => Select(action));

            actionList.Add(button.gameObject);
        }

        UpdateGrid();
        actionList.SelectFirst();
    }

    private void UpdateGrid()
    {
        foreach (Transform child in gridMetodologias.transform)
        {
            Destroy(child.gameObject);
        }

        var selectedActions = GameManager.PlayerData
            .GetSelectedMethodologiesByType(_types[_typeSelectedId]);

        foreach (var action in selectedActions)
        {
            var button = Instantiate(acaoIconPrefab, gridMetodologias.transform);
            button.Acao = action;
        }
    }

    private void Select(ClassAcao action)
    {
        string currentType = _types[_typeSelectedId];

        bool alreadySelected = GameManager.PlayerData.SelectedMethodologyIds.Contains(action.id);
        int selectedCountOfType = GameManager.PlayerData.CountSelectedMethodologiesByType(currentType);

        if (!alreadySelected && selectedCountOfType >= 3)
        {
            return;
        }

        GameManager.PlayerData.ToggleMethodology(action);
        GameManager.Save();

        UpdateGrid();

        selectedCountOfType = GameManager.PlayerData.CountSelectedMethodologiesByType(currentType);

        if (selectedCountOfType != 3)
        {
            return;
        }

        if (!GameManager.PlayerData.HasNineMethodologies())
        {
            ShowConfirmation();
        }
        else
        {
            ShowEndingConfirmation();
        }
    }

    private void ShowConfirmation()
    {
        Confirmation.gameObject.SetActive(true);

        Confirmation.ActionsToShow = GameManager.PlayerData
            .GetSelectedMethodologiesByType(_types[_typeSelectedId]);

        Confirmation.OnAccept(GoToNextMethodology);
        Confirmation.OnDeny(() => Confirmation.gameObject.SetActive(false));
    }

    private void ShowEndingConfirmation()
    {
        EndConfirmation.gameObject.SetActive(true);
    }

    public void Undo()
    {
        string currentType = _types[_typeSelectedId];

        if (GameManager.PlayerData.CountSelectedMethodologiesByType(currentType) != 0)
        {
            GameManager.PlayerData.RemoveMethodologiesByType(currentType);
            GameManager.Save();
            UpdateGrid();
        }
        else if (_typeSelectedId >= 1)
        {
            _typeSelectedId -= 2;
            GoToNextMethodology();
        }
    }
}

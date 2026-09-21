using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionListWrapper : MonoBehaviour
{
    private Animator _animator;

    public SimpleScroll actionList;
    public ControladorSalaDeAula controladorSalaDeAula;

    public Button typeButtonPrefab;
    public GameObject Categories;

    public AcaoIcon acaoIconPrefabDialogo;
    public AcaoIcon acaoIconPrefabSala;
    public AcaoIcon acaoIconPrefabRecursos;

    public void Show()
    {
        _animator.SetTrigger("Show");
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void ShowActions(string tipo)
    {
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Vertical;

        actionList.Clear();

        var buttonList = new List<GameObject>();

        var selectedActions = GameManager.PlayerData.GetSelectedMethodologiesByType(tipo);

        foreach (var acao in selectedActions)
        {
            var button = Instantiate(
                acao.tipo == "Diálogos"
                    ? acaoIconPrefabDialogo
                    : acao.tipo == "Recursos"
                        ? acaoIconPrefabRecursos
                        : acaoIconPrefabSala
            );

            button.Acao = acao;

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                controladorSalaDeAula.UseAction(acao);
            });

            button.GetComponent<Button>().navigation = nav;

            buttonList.Add(button.gameObject);
        }

        actionList.AddList(buttonList);

        if (buttonList.Count > 0)
        {
            actionList.SelectFirst();
        }

        ShowActions();
    }

    public void ShowActions()
    {
        _animator.SetTrigger("Actions");
    }

    public void Return()
    {
        _animator.SetTrigger("Return");
    }

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
}

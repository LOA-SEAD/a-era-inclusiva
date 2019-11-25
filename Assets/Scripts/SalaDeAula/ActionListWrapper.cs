using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionListWrapper : MonoBehaviour
{
    private Animator _animator;
    public SimpleScroll actionList;
    public ControladorSalaDeAula controladorSalaDeAula;
    private List<String> _types;
    public Button typeButtonPrefab;
    private bool _loaded;
    public GameObject Categories;

    public AcaoIcon acaoIconPrefabDialogo;
    public AcaoIcon acaoIconPrefabSala;
    public AcaoIcon acaoIconPrefabRecursos;    // Start is called before the first frame update
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
        foreach (var acao in GameManager.PlayerData.SelectedActions.Where(x=>x.tipo == tipo))
        {
            var button = Instantiate(acao.tipo=="Diálogos"? acaoIconPrefabDialogo : acao.tipo=="Recursos"? acaoIconPrefabRecursos : acaoIconPrefabSala);
            button.Acao = acao;
            button.GetComponent<Button>().onClick.AddListener((() => controladorSalaDeAula.UseAction(acao)));
            button.GetComponent<Button>().navigation = nav;
            buttonList.Add(button.gameObject);
        }
        actionList.AddList(buttonList);
        actionList.SelectFirst();
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
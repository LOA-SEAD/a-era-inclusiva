using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionListWrapperHTPI : MonoBehaviour
{
    private Animator _animator;
    public SimpleScroll actionList;
    public HTPIController controladorHTPI;
    private bool _loaded;
    private ClassAcao Selected = null;
    private Dictionary<ClassAcao, Button> buttonByAction;
    public AcaoIcon acaoIconPrefabDialogo;
    public AcaoIcon acaoIconPrefabSala;
    public AcaoIcon acaoIconPrefabRecursos;

    // Start is called before the first frame update
    public void Show()
    {
        _animator.SetTrigger("Show");
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void BackToTop()
    {
        if(Selected!=null) 
            buttonByAction[Selected].interactable = true;
        if (controladorHTPI.ActionSelected() != null)
        {
            buttonByAction[controladorHTPI.ActionSelected()].interactable = false;
            Selected = controladorHTPI.ActionSelected();
        }

        actionList.BackToTop();
    }

    private void Setup(object obj, EventArgs empty)
    {
    
       actionList.Clear();
        var buttonList = new List<GameObject>();
        var acoes = GameManager.GameData.Acoes.Where(x => x.diaMin <= GameManager.PlayerData.Day).OrderBy(x => x.tipo);
        
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Vertical;
        foreach (var acao in acoes)
        {
            var button = Instantiate(acao.tipo=="Diálogos"? acaoIconPrefabDialogo : acao.tipo=="Recursos"? acaoIconPrefabRecursos : acaoIconPrefabSala);
            buttonByAction[acao] = button.GetComponent<Button>();
            button.Acao = acao;
            button.GetComponent<Button>().navigation = nav;
            button.GetComponent<Button>().onClick.AddListener((() =>
            {
                controladorHTPI.SelectAction(acao);
                
            }));
            buttonList.Add(button.gameObject);
        }
        actionList.AddList(buttonList);
        actionList.SelectFirst();
        
        Navigation navDownButton = new Navigation();
        navDownButton.mode = Navigation.Mode.Explicit;
        navDownButton.selectOnUp = buttonByAction[acoes.Last()].GetComponent<Button>();
        actionList.DownButton.navigation = navDownButton;
        
        Navigation navUpButton = new Navigation();
        navUpButton.mode = Navigation.Mode.Explicit;
        navUpButton.selectOnDown = buttonByAction[acoes.First()].GetComponent<Button>();
        actionList.UpButton.navigation = navUpButton;
    }
    

    public void Start()
    {
        buttonByAction = new Dictionary<ClassAcao, Button>();
        _animator = GetComponent<Animator>();
        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(this, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
        
    }
    

    public void Update()
    {
        
    }
}
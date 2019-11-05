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
    public Button acaoIconPrefab;
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
        foreach (var acao in GameManager.GameData.Acoes.Where(x=>x.diaMin <= GameManager.PlayerData.Day))
        {
            var button = Instantiate(acaoIconPrefab);
            buttonByAction[acao] = button;
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(acao.icone + " " + acao.nome);
            button.onClick.AddListener((() =>
            {
                controladorHTPI.SelectAction(acao);
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (gameObject);
                
            }));
            buttonList.Add(button.gameObject);
        }
        actionList.AddList(buttonList);
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
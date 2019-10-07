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
    public Button typeButtonPrefab;
    private bool _loaded;

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


    public void Start()
    {
        _animator = GetComponent<Animator>();
        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(this, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
        
    }

    private void Setup(object obj, EventArgs empty)
    {
        actionList.Clear();
        var buttonList = new List<GameObject>();
        foreach (var acao in GameManager.GameData.Acoes)
        {
            var button = Instantiate(acaoIconPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(acao.icone + " " + acao.nome);
            button.onClick.AddListener((() => controladorHTPI.SelectAction(acao)));
            buttonList.Add(button.gameObject);
        }
        actionList.AddList(buttonList);
    }


    public void Update()
    {
        
    }
}
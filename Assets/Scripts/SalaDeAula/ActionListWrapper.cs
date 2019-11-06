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

    public AcaoIcon acaoIconPrefab;
    // Start is called before the first frame update
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
        actionList.Clear();
        var buttonList = new List<GameObject>();
        foreach (var acao in GameManager.PlayerData.SelectedActions.Where(x=>x.tipo == tipo))
        {
            var button = Instantiate(acaoIconPrefab);
            button.Acao = acao;
            button.GetComponent<Button>().onClick.AddListener((() => controladorSalaDeAula.UseAction(acao)));
            buttonList.Add(button.gameObject);
        }
        actionList.AddList(buttonList);
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
        _types = new List<string>();
        _animator = GetComponent<Animator>();

        
    }

    private void setCategories()
    {
        foreach (var acao in GameManager.GameData.Acoes.Where(acao => !_types.Contains(acao.tipo)))
        {
            _types.Add(acao.tipo);
            var button = Instantiate(typeButtonPrefab, Categories.transform);
            button.onClick.AddListener(() => ShowActions(acao.tipo));
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(acao.tipo);
        }

        _loaded = true;
    }

    public void Update()
    {
        if (!_loaded && GameManager.GameData.Loaded)
        {
            setCategories();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionListWrapperHTPI : MonoBehaviour
{
    private Animator _animator;

    public SimpleScroll actionList;
    public HTPIController controladorHTPI;

    private Dictionary<ClassAcao, Button> buttonByAction;
    private Dictionary<ClassAcao, PairVisualTag> pairTagByAction;

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

    public void BackToTop()
    {
        actionList.BackToTop();
        RefreshVisualState();
    }

    private void Setup(object obj, EventArgs empty)
    {
        actionList.Clear();

        buttonByAction.Clear();
        pairTagByAction.Clear();

        var buttonList = new List<GameObject>();

        var acoes = GameManager.GameData.Acoes
            .Where(x => x.diaMin <= GameManager.PlayerData.Day)
            .OrderBy(x => x.tipo)
            .ToList();

        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Vertical;

        foreach (var acao in acoes)
        {
            var acaoIcon = Instantiate(
                acao.tipo == "Diálogos" ? acaoIconPrefabDialogo :
                acao.tipo == "Recursos" ? acaoIconPrefabRecursos :
                acaoIconPrefabSala
            );

            Button button = acaoIcon.GetComponent<Button>();

            buttonByAction[acao] = button;

            PairVisualTag pairTag = acaoIcon.GetComponent<PairVisualTag>();

            if (pairTag != null)
            {
                pairTagByAction[acao] = pairTag;
                pairTag.ClearPair();
            }

            acaoIcon.Acao = acao;

            button.navigation = nav;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                controladorHTPI.SelectAction(acao);
            });

            buttonList.Add(acaoIcon.gameObject);
        }

        actionList.AddList(buttonList);
        actionList.SelectFirst();

        if (acoes.Count > 0)
        {
            Navigation navDownButton = new Navigation();
            navDownButton.mode = Navigation.Mode.Explicit;
            navDownButton.selectOnUp = buttonByAction[acoes.Last()];
            actionList.DownButton.navigation = navDownButton;

            Navigation navUpButton = new Navigation();
            navUpButton.mode = Navigation.Mode.Explicit;
            navUpButton.selectOnDown = buttonByAction[acoes.First()];
            actionList.UpButton.navigation = navUpButton;
        }

        RefreshVisualState();
    }

    public void RefreshVisualState()
    {
        foreach (var item in buttonByAction)
        {
            ClassAcao acao = item.Key;
            Button button = item.Value;

            // IMPORTANTE:
            // A ação nunca fica bloqueada, porque pode ser usada em mais de uma demanda.
            button.interactable = true;

            if (pairTagByAction.ContainsKey(acao))
            {
                pairTagByAction[acao].ClearPair();
            }
        }

        ClassDemanda demandaSelecionada = controladorHTPI.DemandSelected();

        if (demandaSelecionada == null)
        {
            return;
        }

        ClassAcao acaoSelecionadaDaDemanda = controladorHTPI.GetActionOfDemand(demandaSelecionada);

        if (acaoSelecionadaDaDemanda == null)
        {
            return;
        }

        int pairNumber = controladorHTPI.GetPairNumber(demandaSelecionada);

        if (pairTagByAction.ContainsKey(acaoSelecionadaDaDemanda))
        {
            pairTagByAction[acaoSelecionadaDaDemanda].SetPairNumber(pairNumber);
        }
    }

    public void Start()
    {
        buttonByAction = new Dictionary<ClassAcao, Button>();
        pairTagByAction = new Dictionary<ClassAcao, PairVisualTag>();

        _animator = GetComponent<Animator>();

        if (GameManager.GameData != null &&
            GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
        {
            Setup(this, EventArgs.Empty);
        }
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }
}
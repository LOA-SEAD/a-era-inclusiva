using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class HTPIController : MonoBehaviour
{
    public BotaoDemandaHTPI _botaoDemanda;
    public Dictionary<ClassDemanda, ClassAcao> _resolucoes;

    public Confirmation confirmation;
    public ActionListWrapperHTPI actionList;
    public ScrollHTPI ScrollHtpi;
    public GameObject content;

    private List<ClassDemanda> _demandasDoDia;

    void Awake()
    {
        if (content != null)
        {
            content.SetActive(false);
        }

        _resolucoes = new Dictionary<ClassDemanda, ClassAcao>();
        _demandasDoDia = new List<ClassDemanda>();

        AudioManager.instance.PlayAmbience((int)SoundType.AmbienceHallway);
        AudioManager.instance.PlayMusic((int)SoundType.MusicTablet);
    }

    void Start()
    {
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

    private void Setup(Object obj, EventArgs empty)
    {
        _resolucoes.Clear();

        _demandasDoDia = GameManager.GetDemandsOfTheDay();

        foreach (var demanda in _demandasDoDia)
        {
            _resolucoes[demanda] = null;
        }

        CarregarResolucoesSalvas();
    }

    private void CarregarResolucoesSalvas()
    {
        if (GameManager.PlayerData == null ||
            GameManager.PlayerData.SelectedResolutions == null ||
            GameManager.GameData == null ||
            GameManager.GameData.Acoes == null)
        {
            return;
        }

        foreach (var resolucao in GameManager.PlayerData.SelectedResolutions)
        {
            if (resolucao.DemandIndex < 0 || resolucao.DemandIndex >= _demandasDoDia.Count)
            {
                continue;
            }

            ClassDemanda demanda = _demandasDoDia[resolucao.DemandIndex];
            ClassAcao acao = GameManager.GameData.Acoes.FirstOrDefault(x => x.id == resolucao.ActionId);

            if (acao != null)
            {
                _resolucoes[demanda] = acao;
            }
        }
    }

    public void SelectDemand(BotaoDemandaHTPI botaoDemanda)
    {
        _botaoDemanda = botaoDemanda;

        if (actionList != null)
        {
            actionList.GetComponent<Animator>().SetTrigger("Actions");
            actionList.RefreshVisualState();
        }
    }

    public void SelectAction(ClassAcao acao)
    {
        if (_botaoDemanda == null || _botaoDemanda.Demanda == null || acao == null)
        {
            return;
        }

        ClassDemanda demanda = _botaoDemanda.Demanda;

        _resolucoes[demanda] = acao;

        int demandIndex = _demandasDoDia.IndexOf(demanda);

        if (demandIndex >= 0)
        {
            GameManager.PlayerData.SetResolution(demandIndex, acao.id);
            GameManager.Save();
        }

        int pairNumber = GetPairNumber(demanda);

        _botaoDemanda.Select(pairNumber);

        Button botao = _botaoDemanda.GetComponent<Button>();

        if (botao != null)
        {
            botao.interactable = false;
        }

        if (actionList != null)
        {
            actionList.RefreshVisualState();
        }

        if (ScrollHtpi != null)
        {
            ScrollHtpi.DemandList.GoDown();
        }

        if (TodasDemandasResolvidas())
        {
            Confirmation();
        }
    }

    private bool TodasDemandasResolvidas()
    {
        return _resolucoes.Count(x => x.Value != null) == _demandasDoDia.Count;
    }

    public int GetPairNumber(ClassDemanda demanda)
    {
        if (demanda == null || _demandasDoDia == null)
        {
            return 0;
        }

        int index = _demandasDoDia.IndexOf(demanda);

        if (index < 0)
        {
            return 0;
        }

        return index + 1;
    }

    public ClassDemanda DemandSelected()
    {
        if (_botaoDemanda == null)
        {
            return null;
        }

        return _botaoDemanda.Demanda;
    }

    public ClassAcao ActionSelected()
    {
        ClassDemanda demanda = DemandSelected();

        if (demanda == null || !_resolucoes.ContainsKey(demanda))
        {
            return null;
        }

        return _resolucoes[demanda];
    }

    public ClassAcao GetActionOfDemand(ClassDemanda demanda)
    {
        if (demanda == null || !_resolucoes.ContainsKey(demanda))
        {
            return null;
        }

        return _resolucoes[demanda];
    }

    public bool DemandIsResolved(ClassDemanda demanda)
    {
        return demanda != null &&
               _resolucoes.ContainsKey(demanda) &&
               _resolucoes[demanda] != null;
    }

    public void Confirmation()
    {
        confirmation.gameObject.SetActive(true);

        confirmation.AcceptButton.onClick.RemoveAllListeners();
        confirmation.AcceptButton.onClick.AddListener(() =>
        {
            GameManager.Save();
        });
    }
}
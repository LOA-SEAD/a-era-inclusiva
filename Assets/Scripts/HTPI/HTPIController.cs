using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class HTPIController : MonoBehaviour
{
    private bool _loaded;
    public BotaoDemandaHTPI _botaoDemanda;
    public Dictionary<ClassDemanda, ClassAcao> _resolucoes;
    public Confirmation confirmation;
    public ActionListWrapperHTPI actionList;
    public ScrollHTPI ScrollHtpi;
    public GameObject content;
    void Awake()
    {
        content.SetActive(false);
        
        _resolucoes = new Dictionary<ClassDemanda, ClassAcao>();

        AudioManager.instance.PlayAmbience((int) SoundType.AmbienceHallway);
        AudioManager.instance.PlayMusic((int)SoundType.MusicTablet);
    }

    void Start()
    {
        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(this, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }

    private void Setup(Object obj, EventArgs empty)
    {
        foreach (var demand in GameManager.GetDemandsOfTheDay())
        {
            _resolucoes[demand] = null;
        }
        GameManager.PlayerData.Day++;
    }

    public void SelectDemand(BotaoDemandaHTPI BotaoDemanda)
    {
        actionList.GetComponent<Animator>().SetTrigger("Actions");
        _botaoDemanda = BotaoDemanda;
    }

    public void SelectAction(ClassAcao acao)
    {
        _resolucoes[_botaoDemanda.Demanda] = acao;
        _botaoDemanda.Select();
        ScrollHtpi.DemandList.GoDown();

        if (_resolucoes.Count(x => x.Value!=null) == GameManager.GameData.Demandas.Count)
        {
            Confirmation();
        }
    }

    public ClassAcao ActionSelected()
    {
        return _resolucoes.ContainsKey(_botaoDemanda.Demanda) ? _resolucoes[_botaoDemanda.Demanda] : null;
    }

    public void Confirmation()
    {
        confirmation.gameObject.SetActive(true);
    }
}
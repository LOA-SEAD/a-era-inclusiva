using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    private bool _loaded;
    public BotaoDemandaHTPI buttonPrefab;
    public SimpleScroll demandList;
    private ClassDemanda _demanda;
    public Dictionary<ClassDemanda, ClassAcao> _resolucoes;
    private Dictionary<ClassDemanda, BotaoDemandaHTPI> _botaoPorDemanda;
    public Confirmation confirmation;
    public ActionListWrapperHTPI actionList;

    public GameObject content;
    // Start is called before the first frame update
    void Awake()
    {
        _botaoPorDemanda = new Dictionary<ClassDemanda, BotaoDemandaHTPI>();
        content.SetActive(false);
        actionList.gameObject.SetActive(false);
        demandList.gameObject.SetActive(false);
        _resolucoes = new Dictionary<ClassDemanda, ClassAcao>();
    
    }



    public void SelectDemand(ClassDemanda demanda)
    {
        actionList.GetComponent<Animator>().SetTrigger("Actions");
        _demanda = demanda;
        actionList.actionList.BackToTop();
    }

    public void SelectAction(ClassAcao acao)
    {
        _resolucoes[_demanda] = acao;
        _botaoPorDemanda[_demanda].Select();

        if (_resolucoes.Count == GameManager.GameData.Demandas.Count)
        {

            Debug.Log(GameManager.GameData.Demandas.Count);
            Confirmation();
        }
    }

    public void Confirmation()
    {
        confirmation.gameObject.SetActive(true);
    }

    
    // Update is called once per frame
}
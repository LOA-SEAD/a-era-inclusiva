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
    public SimpleScrollAlt demandList;
    private ClassDemanda _demanda;
    public Dictionary<ClassDemanda, ClassAcao> _resolucoes;
    public Confirmation confirmation;
    public ActionListWrapperHTPI actionList;
    public ScrollHTPI scrollhtpi;
    public GameObject content;
    // Start is called before the first frame update
    void Awake()
    {
        content.SetActive(false);
        actionList.gameObject.SetActive(false);

        _resolucoes = new Dictionary<ClassDemanda, ClassAcao>();
    
    }



    public void SelectDemand(ClassDemanda demanda)
    {
        actionList.GetComponent<Animator>().SetTrigger("Actions");
        _demanda = demanda;
    }

    public void SelectAction(ClassAcao acao)
    {
   
        _resolucoes[_demanda] = acao;
        scrollhtpi.DemandList.GoDown();

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
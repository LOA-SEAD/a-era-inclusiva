using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    private bool _loaded;
    public BotaoDemandaHTPI buttonPrefab;
    public SimpleScroll demandList;
    private ClassDemanda _demanda;
    private Dictionary<ClassDemanda, ClassAcao> _resolucoes;
    private Dictionary<ClassDemanda, BotaoDemandaHTPI> _botaoPorDemanda;        

    public ActionListWrapperHTPI actionList;

    // Start is called before the first frame update
    void Awake()
    {
        _resolucoes = new Dictionary<ClassDemanda, ClassAcao>();
        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(null, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }

    private void Start()
    {
        _botaoPorDemanda = new Dictionary<ClassDemanda, BotaoDemandaHTPI>();
        actionList.gameObject.SetActive(false);
        demandList.gameObject.SetActive(false);
    }

    void Setup(object sender, EventArgs e)
    {
        var buttonList = new List<GameObject>();
        foreach (ClassDemanda demanda in GameManager.GameData.Demandas)
        {
            var button = Instantiate(buttonPrefab);
            button.SetDemand(demanda);
            button.GetComponent<Button>().onClick.AddListener(() => SelectDemand(demanda));
            buttonList.Add(button.gameObject);
            _botaoPorDemanda[demanda] = button;
        }

        demandList.AddList(buttonList);
    }

    void SelectDemand(ClassDemanda demanda)
    {
        Debug.Log("1");
        actionList.GetComponent<Animator>().SetTrigger("Show");
        _demanda = demanda;
    }

    public void SelectAction(ClassAcao acao)
    {
        _resolucoes[_demanda] = acao;
        _botaoPorDemanda[_demanda].Select();
    }

    // Update is called once per frame
}
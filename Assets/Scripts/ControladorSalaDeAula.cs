using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;
using Random = System.Random;

public class ControladorSalaDeAula : MonoBehaviour
{
    //Representação dos objetos da tela
    public Transform painelAlunos;
    public Transform painelAcoes;
    public Transform painelDescDemandas;
    public ActionButton prefabBotaoAcao;
    public DemandToggle prefabBotaoDemanda;
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";
    private ClassDemanda selectedDemand;


    public int delayBetweenEachDemand;
    public SpeechBubble speechBubble;

    private Time _lastDemand;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var action in Game.Actions.acoes.Where(x=>x.selected))
        {
            if (action.selected)
            {
                var button = Instantiate(prefabBotaoAcao, painelAcoes);
                button.GetComponent<Button>().onClick.AddListener(delegate { UseAction(action); });
                button.GetComponent<Button>().interactable = false;
                button.Action = action;
            }
        }

    }

    void Start()
    {
        StartCoroutine(SpawnDemands());
    }
    

    // Update is called once per frame
    IEnumerator SpawnDemands()
    {
        //ClassDemanda demanda;
        var studentList = Game.DemandingStudents;
        var demandList = Game.Demands.demandas.Where(x => studentList.Select(y=>y.id).Contains(x.idAluno) && !x.resolvida).ToList();
        while (demandList.Any())
        {
            yield return new WaitForSeconds(delayBetweenEachDemand);

            if (painelDescDemandas.childCount > 2)
            {
                Destroy(painelDescDemandas.GetChild(2).gameObject);
            }

            var demanda = demandList.First();
            demandList.RemoveAt(0);
            demanda.resolvida = true;
            var button = Instantiate(prefabBotaoDemanda, painelDescDemandas);
            button.gameObject.transform.SetAsFirstSibling();
            button.GetComponent<Button>().onClick.AddListener(delegate { SelectDemand(demanda); });

            button.Demand = demanda;
            button.Level = demanda.nivelUrgencia;
            button.Student = demanda.student;
            speechBubble.gameObject.SetActive(true);
            speechBubble.SetText(demanda.descricao);
        }

    }

    private void SelectDemand(ClassDemanda demand)
    {

 
        if (selectedDemand == null)
        {
            foreach (var button in painelAcoes.GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }
        }
        selectedDemand = demand;

        speechBubble.gameObject.SetActive(true);
        speechBubble.SetText(demand.descricao);

    }
    private void UseAction(ClassAcao action)
    {        Debug.Log(action.nome+" e " + selectedDemand.descricao);

    }
}

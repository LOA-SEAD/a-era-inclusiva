using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ControladorSalaDeAula : MonoBehaviour
{
    public Transform demandPanel;
    public Transform actionPanel;
    public ActionButton prefabActionButton;
    public DemandToggle prefabDemandToggle;
    public ClassDemanda selectedDemand;
    public int delayBetweenEachDemand;
    public SpeechBubble speechBubble;

    private Time _lastDemand;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var action in Game.Actions.acoes.Where(x=>x.selected))
        {
                var button = Instantiate(prefabActionButton, actionPanel);
                button.GetComponent<Button>().onClick.AddListener(delegate { UseAction(action); });
                button.enabled = false;
                button.Action = action;
        }

    }

    void Start()
    {
        StartCoroutine(SpawnDemands());

    }
    

    // Update is called once per frame
    IEnumerator SpawnDemands()
    {
        ClassAluno student;
        List<ClassAluno> studentList = Game.Students.alunos.FindAll(x => x.importante);
        Random rand = new Random();

        while (true)
        {
            yield return new WaitForSeconds(delayBetweenEachDemand);

            if (demandPanel.childCount > 2)
            {
                Destroy(demandPanel.GetChild(2).gameObject);
            }
            student = studentList[rand.Next() % studentList.Count];
            var demands = Game.Demands.demandas.FindAll(x => x.aluno == student.nome);
            var idDemand = rand.Next() % demands.Count;
            
            var button = Instantiate(prefabDemandToggle, demandPanel);
            button.gameObject.transform.SetAsFirstSibling();
            var demands1 = demands; // é perigoso acessar direto, os valores podem mudar (pelo menos foi isso q o rider me disse)
            var demand = idDemand;
            button.GetComponent<Button>().onClick.AddListener(delegate { SelectDemand(demands1[demand]); });
            button.Student = student;
            button.Level = rand.Next(3);
            speechBubble.gameObject.SetActive(true);
            speechBubble.SetText(demands[idDemand].frase);
        }

    }

    private void SelectDemand(ClassDemanda demand)
    {
 
        if (selectedDemand == null)
        {
            foreach (var button in actionPanel.GetComponentsInChildren<Button>())
            {
                button.enabled = true;
            }
        }
        selectedDemand = demand;
        speechBubble.gameObject.SetActive(true);
        speechBubble.SetText(demand.frase);
     

    }

    private void UseAction(ClassAcao action)
    {        Debug.Log(action.nome+" e " + selectedDemand.descricao);

    }
}

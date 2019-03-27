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
    public Button prefabBotaoAcao;
    public GameObject prefabDescDemanda;
    public DemandToggle prefabBotaoDemanda;
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";

    public ClassAluno alunoSelecionado;

    public int delayBetweenEachDemand;
    public SpeechBubble speechBubble;

    private Time _lastDemand;

    private string textoDemanda;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var action in Game.Actions.acoes.Where(x=>x.selected))
        {
            if (acao.selected)
            {
                var button = Instantiate(prefabBotaoAcao, painelAcoes);
                button.onClick.AddListener(delegate { usarAcao(acao);});
                button.GetComponentInChildren<TextMeshProUGUI>().SetText(acao.nome);
            }
        }

    }

    void Start()
    {
        textoDemanda = "";
        StartCoroutine(SpawnDemands());
    }

    private void showDemand(int idAluno)
    {
        for (int i = 0; i < Game.Demands.Count; i++)
        {
            if (Game.Demands[i].idaluno == idAluno && !Game.Demands[i].resolvida)
            {
                textoDemanda = Game.Demands[i].descricao;
                Game.Demands[i].resolvida = true;
                break;
            }
        }

        var prefabDemanda = Instantiate(prefabDescDemanda, painelDescDemandas);
        prefabDemanda.GetComponentInChildren<TextMeshProUGUI>().text = textoDemanda;
    }
    

    // Update is called once per frame
    IEnumerator SpawnDemands()
    {
        //ClassDemanda demanda;
        ClassAluno student;
        List<ClassAluno> studentList = Game.Students.alunos.FindAll(x => Game.levelDemandingStudents.Contains(x.id));
        Random randomGenerator = new Random();

        while (true)
        {
            yield return new WaitForSeconds(delayBetweenEachDemand);

            if (demandPanel.childCount > 2)
            {
                Destroy(demandPanel.GetChild(2).gameObject);
            }
            student = studentList[randomGenerator.Next() % studentList.Count];
//           demanda = aluno.demandas[randomGenerator.Next() % aluno.demandas.Count];
            var button = Instantiate(prefabBotaoDemanda, painelAlunos);
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
    }
}

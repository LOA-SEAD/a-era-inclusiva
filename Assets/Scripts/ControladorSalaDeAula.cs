using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ControladorSalaDeAula : MonoBehaviour
{
    //Representação dos objetos da tela
    public Transform painelAlunos;

    public Transform painelAcoes;

    public Button prefabBotaoAcao;

    public DemandToggle prefabBotaoDemanda;
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";

    public ClassAluno alunoSelecionado;

    public int delayBetweenEachDemand;

    private Time _lastDemand;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var acao in Game.Actions.acoes)
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
        StartCoroutine(SpawnDemands());

    }
    
    private void usarAcao(ClassAcao acao)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    IEnumerator SpawnDemands()
    {
        ClassDemanda demanda;
        ClassAluno student;
        List<ClassAluno> studentList = Game.Students.alunos.FindAll(x => x.importante);
        Random randomGenerator = new Random();

        while (true)
        {
            if (painelAlunos.childCount > 2)
            {
                Destroy(painelAlunos.GetChild(2).gameObject);
            }
            student = studentList[randomGenerator.Next() % studentList.Count];
//            demanda = aluno.demandas[randomGenerator.Next() % aluno.demandas.Count];
            var button = Instantiate(prefabBotaoDemanda, painelAlunos);
            button.gameObject.transform.SetAsFirstSibling();
            button.Student = student;
            button.Level = randomGenerator.Next(3);
            yield return new WaitForSeconds(delayBetweenEachDemand);

        }



    }
}

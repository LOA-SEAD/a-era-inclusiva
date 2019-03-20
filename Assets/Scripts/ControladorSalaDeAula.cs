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

    public Button prefabBotaoDemanda;
    public Image studentPhoto;
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";

    public ClassAluno alunoSelecionado;

    public int delayBetweenEachDemand;

    private Time lastDemand;
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
        StartCoroutine(SpawnDemand());

    }
    
    private void usarAcao(ClassAcao acao)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    IEnumerator SpawnDemand()
    {
        ClassDemanda demanda;
        ClassAluno aluno;
        List<ClassAluno> listaAlunos = Game.Students.alunos.FindAll(x => x.importante);
        Random randomGenerator = new Random();

        while (true)
        {
            if (painelAlunos.childCount > 4)
            {
                Destroy(painelAlunos.GetChild(4).gameObject);
            }
            aluno = listaAlunos[randomGenerator.Next() % listaAlunos.Count];
//            demanda = aluno.demandas[randomGenerator.Next() % aluno.demandas.Count];
            var button = Instantiate(prefabBotaoDemanda, painelAlunos);
            button.gameObject.transform.SetAsFirstSibling();
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(aluno.nome);
            yield return new WaitForSeconds(delayBetweenEachDemand);

        }



    }
}

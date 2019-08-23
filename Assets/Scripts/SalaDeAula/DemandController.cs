using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DemandController : MonoBehaviour
{
    public float delayBetweenEachDemand;

    public DemandToggle prefabBotaoDemanda;
    public ControladorSalaDeAula controller;
    public SimpleScroll simpleScroll;
    public GameObject actionListWrapper ;
    public AudioSource demandSound;
    public List<AudioClip> AudioClips;

    public AlunosSalaDeAula alunosSalaDeAula;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDemands());
    }

    IEnumerator SpawnDemands()
    {
        var studentList = GameManager.GameData.Students.alunos.Where(x=>x.importante);
        var demandList = GameManager.GameData.Demands.demandas
            .Where(x => studentList.Select(y => y.id).Contains(x.idAluno) && !x.resolvida).OrderBy(x => x.ordem)
            .ToList();
        while (demandList.Any())
        {
            yield return new WaitForSeconds(delayBetweenEachDemand);

            SpawnDemand(demandList.First());
            demandList.RemoveAt(0);
        }
    }

    private void SpawnDemand(ClassDemanda demanda)
    {
        var button = Instantiate(prefabBotaoDemanda);
        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            actionListWrapper.GetComponent<Animator>().SetTrigger("Show");
        });
        button.Demand = demanda;
        alunosSalaDeAula.MostrarBalao(demanda);
        demandSound.clip = AudioClips[demanda.nivelUrgencia - 1];
        demandSound.Play();
        //controller.Speak(demanda.descricao);
        simpleScroll.Add(button.gameObject);
    }
}

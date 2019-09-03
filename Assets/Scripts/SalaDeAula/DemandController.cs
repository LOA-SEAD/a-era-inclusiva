using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DemandController : MonoBehaviour
{
    public GameObject actionListWrapper;

    public AlunosSalaDeAula alunosSalaDeAula;
    public List<AudioClip> AudioClips;
    public ControladorSalaDeAula controller;
    public float delayBetweenEachDemand;
    public AudioSource demandSound;

    public DemandToggle prefabBotaoDemanda;

    public SimpleScroll simpleScroll;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnDemands());
    }

    private IEnumerator SpawnDemands()
    {
        var studentList = GameManager.GameData.Alunos.Where(x => x.importante);
        var demandList = GameManager.GameData.Demandas
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
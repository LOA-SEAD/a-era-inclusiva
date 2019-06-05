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
    public GameObject categoriesMenu, actionListMenu ;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDemands());
    }

    IEnumerator SpawnDemands()
    {
        var studentList = Game.DemandingStudents;
        var demandList = Game.Demands.demandas
            .Where(x => studentList.Select(y => y.id).Contains(x.idAluno) && !x.resolvida).OrderBy(x => x.ordem)
            .ToList();
        while (demandList.Any())
        {
            yield return new WaitForSeconds(delayBetweenEachDemand);

            var demanda = demandList.First();
            demandList.RemoveAt(0);
            var button = Instantiate(prefabBotaoDemanda);
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                actionListMenu.SetActive(false);
                categoriesMenu.SetActive(true);
            });
            button.Demand = demanda;
            //controller.Speak(demanda.descricao);
            simpleScroll.Add(button.gameObject);
        }
    }

}

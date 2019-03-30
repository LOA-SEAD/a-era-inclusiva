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

            if (transform.childCount > 2)
            {
                Destroy(transform.GetChild(2).gameObject);
            }

            var demanda = demandList.First();
            demandList.RemoveAt(0);
            demanda.resolvida = true;
            var button = Instantiate(prefabBotaoDemanda, transform);
            button.gameObject.transform.SetAsFirstSibling();
            button.Demand = demanda;
            controller.Speak(demanda.descricao);
        }
    }

}

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class DemandController : MonoBehaviour
{
    public ActionListWrapper actionListWrapper;

    public AlunosSalaDeAula alunosSalaDeAula;
    public ControladorSalaDeAula controller;
    public float minDelay;
    public float maxDelay;

    public DemandToggle prefabBotaoDemanda;

    public SimpleScroll simpleScroll;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnDemands());
    }

    private IEnumerator SpawnDemands()
    {
        // Precisa dessa linha de baixo? Perguntar ao João
        yield return new WaitForSeconds(5);

        var studentList = GameManager.GameData.Alunos.Where(x => x.importante);
        var demandList = GameManager.GameData.Demandas
            .Where(x => studentList.Select(y => y.id).Contains(x.idAluno) && !x.resolvida).OrderBy(x => x.ordem)
            .ToList();
        while (demandList.Any())
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            SpawnDemand(demandList.First());
            demandList.RemoveAt(0);
        }
    }

    private void SpawnDemand(ClassDemanda demanda)
    {
        var button = Instantiate(prefabBotaoDemanda);
        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            actionListWrapper.Show();
        });
        button.Demand = demanda;
        alunosSalaDeAula.MostrarBalao(demanda);
        //PlayDemandSound(demanda.nivelUrgencia, button.transform.position.x, button.transform.position.y);
        PlayDemandSound(demanda.nivelUrgencia);
        simpleScroll.Add(button.gameObject);
    }

    private void PlayDemandSound(int urgencia)
    {
        SoundType demandSound;

        switch(urgencia)
        {
            case 1:
                demandSound = SoundType.DemandGreen;
                break;
            case 2:
                demandSound = SoundType.DemandYellow;
                break;
            case 3:
                demandSound = SoundType.DemandRed;
                break;
            default:
                Debug.Log("Non-existent demand sound!");
                return;
        }

        AudioManager.instance.PlaySfx((int)demandSound);
    }
}
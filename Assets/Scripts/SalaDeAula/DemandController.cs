using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class DemandController : MonoBehaviour
{
    public ActionListWrapper actionListWrapper;
    public ControladorSalaDeAula controladorSalaDeAula;
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
    private void OnEnable()
    {
        uimaster.Enable();
    }

    public UIMaster uimaster { get; set; }

    private void OnDisable()
    {
        uimaster.Disable();
    }

    private void Awake()
    {
        uimaster = new UIMaster();
        uimaster.UI.Submit.performed += ctx => TrySelectNextDemand();
    }

    private void TrySelectNextDemand()
    {
        if (controladorSalaDeAula.SelectedDemand != null) return;
            simpleScroll.SelectFirst();
    }

    private IEnumerator SpawnDemands()
    {
        yield return new WaitForSeconds(2);

        var demandList = GameManager.GetDemandsOfTheDay().OrderBy(x => x.ordem).ToList();
        while (demandList.Any())
        {
            SpawnDemand(demandList.First());
            demandList.RemoveAt(0);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

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
        controladorSalaDeAula.HappinessFactor += demanda.nivelUrgencia;
        alunosSalaDeAula.MostrarBalao(demanda);
        //PlayDemandSound(demanda.nivelUrgencia, button.transform.position.x, button.transform.position.y);
        PlayDemandSound(demanda.nivelUrgencia);
        simpleScroll.Add(button.gameObject);
        if (controladorSalaDeAula.SelectedDemand == null)
        {
                button.GetComponent<Button>().Select();
        }
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
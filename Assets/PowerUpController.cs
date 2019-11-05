using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    public ControladorSalaDeAula controladorSalaDeAula;
    public ActionListWrapper actionListWrapper;

    public DemandController demandController;

    public GameObject PowerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowPowerUps()
    {
        var elogiar = Instantiate(PowerUpPrefab );
        elogiar.GetComponentInChildren<TextMeshProUGUI>().SetText("Elogiar");
        elogiar.GetComponent<Button>().onClick.AddListener(Elogiar);
        
        var estagiario = Instantiate(PowerUpPrefab );
        estagiario.GetComponentInChildren<TextMeshProUGUI>().SetText("Chamar Estagiário");
        estagiario.GetComponent<Button>().onClick.AddListener(ChamarEstagiario);
        
        var repostaCerta = Instantiate(PowerUpPrefab );
        repostaCerta.GetComponentInChildren<TextMeshProUGUI>().SetText("Resposta Certa");
        repostaCerta.GetComponent<Button>().onClick.AddListener(RespostaCerta);
        
        actionListWrapper.actionList.Clear();
        
        actionListWrapper.actionList.Add(elogiar);
        actionListWrapper.actionList.Add(estagiario);
        actionListWrapper.actionList.Add(repostaCerta);

    }
    public void Elogiar()
    {
        actionListWrapper.Hide();

        if (GameManager.PlayerData.Points < 50)
        {
            controladorSalaDeAula.Speak("Preciso de pelo menos 50 pontos para utilizar o elogio!");
            return;
        }

        GameManager.PlayerData.Points -= 50;
        StartCoroutine("DisableHappinessDecrease");
        
    }
    public void ChamarEstagiario()
    {
        actionListWrapper.Hide();
        if (GameManager.PlayerData.Points < 100)
        {
            controladorSalaDeAula.Speak("Preciso de pelo menos 100 pontos para chamar o estagiário!");
            return;
        }
        controladorSalaDeAula.Speak("O estagiário vai me ajudar a lidar com a turma, fazendo com que menos demandas surjam!");

        GameManager.PlayerData.Points -= 100;
        demandController.minDelay+=5;
        demandController.maxDelay+=5;

    }

    public void RespostaCerta()
    {
        if (GameManager.PlayerData.Points < 150)
        {
            controladorSalaDeAula.Speak("Preciso de pelo menos 150 pontos para utilizar esse Power-Up!");
            return;
        }
        var action = GameManager.GameData.Acoes.First(y=>y.id == controladorSalaDeAula.SelectedDemand.Demand.acoesEficazes.OrderBy(x=>x.efetividade).First().idAcao);
        controladorSalaDeAula.Speak("Ahhh! me lembrei, a ação correta é: " + action.nome);

        actionListWrapper.Hide();

    }
    public IEnumerator DisableHappinessDecrease()
    {
        controladorSalaDeAula.happinessDecreasePaused = true;
        yield return new WaitForSeconds(30);
        controladorSalaDeAula.happinessDecreasePaused = false;

    }
}
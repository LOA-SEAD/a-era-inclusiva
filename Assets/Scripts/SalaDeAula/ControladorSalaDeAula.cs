using System.Linq;
using UnityEngine;

public class ControladorSalaDeAula : MonoBehaviour
{
    private DemandToggle _selectedDemand;
    public AudioClip acertoClip;
    public ActionListWrapper actionListWrapper;
    public AudioSource audioSource;
    public BarraInferior barraInferior;
    public float levelTimeInSeconds;
    public SceneController sceneController;
    public SpeechBubble speechBubble;

    public DemandToggle SelectedDemand
    {
        set
        {
            _selectedDemand = value;
            Speak(_selectedDemand.Demand.descricao);
        }
    }

    private void Start()
    {
        actionListWrapper.actionList.SetWhenSelected(UseAction);
    }

    private void Update()
    {
        //timer da fase
        levelTimeInSeconds -= Time.deltaTime;
        CheckIfEnd();
    }


    public void UseAction(ClassAcao action)
    {
        actionListWrapper.Hide();

        var demand = _selectedDemand.Demand;

        if (_selectedDemand == null)
        {
            Speak("Não posso fazer isso sem ter escolhido a demanda!");
            return;
        }

        Destroy(_selectedDemand.gameObject);

        var e = demand.acoesEficazes.FirstOrDefault(x => x.idAcao == action.id);
        demand.resolvida = true;
        if (e == null)
        {
            Speak("Acho que isso não funcionou muito bem");
            return;
        }


        audioSource.clip = acertoClip;
        audioSource.Play();
        Speak(e.efetividade);
        barraInferior.IncrementScore(e.efetividade);
        _selectedDemand = null;
        CheckIfEnd();
    }

    private void CheckIfEnd()
    {
        if (GameManager.GameData.demandas.FindAll(x => !x.resolvida).Count == 0 || levelTimeInSeconds <= 0f)
            sceneController.ChangeTo("Scenes/HTPI");
    }

    public void Speak(string demandaDescricao)
    {
        speechBubble.gameObject.SetActive(true);
        speechBubble.SetText(demandaDescricao);
    }

    public void Speak(int points)
    {
        speechBubble.gameObject.SetActive(true);
        speechBubble.ShowResult(points);
    }
}
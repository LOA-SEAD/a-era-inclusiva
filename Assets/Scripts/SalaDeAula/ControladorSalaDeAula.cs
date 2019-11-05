using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControladorSalaDeAula : MonoBehaviour
{
    private DemandToggle _selectedDemand;
    public ActionListWrapper actionListWrapper;
    public BarraInferior barraInferior;
    public float levelTimeInSeconds;
    public SceneController sceneController;
    public SpeechBubble speechBubble;
    public int HappinessFactor = 0;
    public bool happinessDecreasePaused;

    public DemandToggle SelectedDemand
    {
        set
        {
            _selectedDemand = value;
            Speak(_selectedDemand.Demand.descricao);
        }
        get
        {
           return  _selectedDemand;
        }
    }

    private void Start()
    {
        InvokeRepeating("CheckIfEnd", 1, 2);
        StartCoroutine("DecreaseHappiness");

        AudioManager.instance.PlaySfx((int)SoundType.BellRing);
        AudioManager.instance.UnMuteAmbience();
        AudioManager.instance.PlayAmbience((int) SoundType.AmbienceClass);
        AudioManager.instance.StopMusic();
    }

    public IEnumerator DecreaseHappiness()
    {
        while (true)
        {
            while (!happinessDecreasePaused) yield return null;
            GameManager.PlayerData.Happiness -= HappinessFactor;
            yield return new WaitForSeconds(5);
        }
    }
    private void Update()
    {
        //timer da fase
        levelTimeInSeconds -= Time.deltaTime;
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

        HappinessFactor -= _selectedDemand.Demand.nivelUrgencia;
        Destroy(_selectedDemand.gameObject);

        var e = demand.acoesEficazes.FirstOrDefault(x => x.idAcao == action.id);
        demand.resolvida = true;
        if (e == null)
        {
            Speak("Acho que isso não funcionou muito bem");
            AudioManager.instance.PlaySfx((int)SoundType.AnswerWrong);
            return;
        }

        AudioManager.instance.PlaySfx((int)SoundType.AnswerRight);
        Speak(e.efetividade);
        barraInferior.IncrementScore(e.efetividade);
        _selectedDemand = null;
        CheckIfEnd();
    }

    private void CheckIfEnd()
    {
        if (GameManager.GameData.Demandas.FindAll(x => !x.resolvida).Count == 0 || levelTimeInSeconds <= 0f)
        {
            AudioManager.instance.PlaySfx((int)SoundType.BellRing);
            sceneController.ChangeTo("Scenes/HTPI");
            GameManager.PlayerData.SelectedActions = new HashSet<ClassAcao>();
        }
    }

    public void Speak(string demandaDescricao)
    {
        speechBubble.SetText(demandaDescricao);

        speechBubble.gameObject.SetActive(true);
    }

    public void Speak(int points)
    {
        speechBubble.ShowResult(points);
        speechBubble.gameObject.SetActive(true);

    }
}
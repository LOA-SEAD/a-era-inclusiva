using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSalaDeAula : MonoBehaviour
{
    private UIMaster uimaster;
    private DemandToggle _selectedDemand;
    public ActionListWrapper actionListWrapper;
    public BarraInferior barraInferior;
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
        get { return _selectedDemand; }
    }

    private void Start()
    {
        if (GameManager.PlayerData != null)
            Setup(this, EventArgs.Empty);
        else
        {
            SaveManager.DataLoaded += Setup;
        }
    }

    private void Setup(object sender, EventArgs eventArgs)
    {
        InvokeRepeating("CheckIfEnd", 1, 2);
        StartCoroutine("DecreaseHappiness");

        AudioManager.instance.PlaySfx((int) SoundType.BellRing);
        AudioManager.instance.PlayAmbience((int) SoundType.AmbienceClass);
        AudioManager.instance.StopMusic();
        avatar.GetComponent<Image>().sprite = GameManager.GetAvatarImage();
    }

    public IEnumerator DecreaseHappiness()
    {
        while (true)
        {
            while (happinessDecreasePaused) yield return null;
            GameManager.PlayerData.Happiness -= HappinessFactor/3;
            barraInferior.UpdateHappinessIcon();

            yield return new WaitForSeconds(5);
        }
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
            GameManager.PlayerData.Happiness -= 10;
            Speak("Acho que isso não funcionou muito bem");
            AudioManager.instance.PlaySfx((int) SoundType.AnswerWrong);
            barraInferior.UpdateHappinessIcon();

            return;
        }
        Debug.Log("antes "+GameManager.PlayerData.Happiness);

        GameManager.PlayerData.Happiness += e.efetividade / 10;
        barraInferior.UpdateHappinessIcon();

        Debug.Log("depois "+GameManager.PlayerData.Happiness);
        AudioManager.instance.PlaySfx((int) SoundType.AnswerRight);
        Speak(e.efetividade);
        barraInferior.IncrementScore(e.efetividade);
        _selectedDemand = null;
        CheckIfEnd();
        
    }

    private void CheckIfEnd()
    {
        if (GameManager.GameData.Demandas.FindAll(x => !x.resolvida).Count == 0)
        {
            End();
        }
    }

    public void End()
    {
        AudioManager.instance.PlaySfx((int) SoundType.BellRing);
        sceneController.ChangeTo("Scenes/HTPI");
        GameManager.PlayerData.SelectedActions = new HashSet<ClassAcao>();
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